using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, END}
public enum PlayerNames {BLANCHE, FELEMAR, LUCIAN, RUFFLES, SULLY}

public class GameManager : MonoBehaviour
{
  public GameObject mapPrefab;
  public BattleState state;
  public GameObject UIManager;
  public Player[] Players_SO;
  public GameObject[] Players_GO = new GameObject[5];
  public Enemy[] Enemies_SO;
  public GameObject[] Enemies_GO = new GameObject[1];
  // player index is in alphabetical order for now
  // Blanche, Felemar, Lucian, Ruffles, Sully
  // TODO: change this order to be based on initiative rolls
  UIManager UIScript;
  TileMapMouse cursor;
  GameObject map;
  TGMap tileSetter;
  int whosTurn = 0;

  void Start()
  {
    state = BattleState.START;
    SetupBattle();
  }

  void SetupBattle()
  {
    CreateMap();
    CreatePlayers();
    CreateEnemies();
    UIScript = UIManager.GetComponent<UIManager>();
    // TODO: Create a turn tracker and change state dynamically based on initiative rolls
    state = BattleState.PLAYERTURN;
    PlayerTurn();
  }

  void CreateMap()
  {
    map = Instantiate(mapPrefab);
    tileSetter = map.GetComponent<TGMap>();
    cursor = map.GetComponent<TileMapMouse>();
  }

  void CreatePlayers()
  {
    for(int i = 0; i < 5; i++)
    {
      Players_GO[i] = Instantiate(Players_SO[i].playerPrefab, Players_SO[i].spawnPoint, Quaternion.identity);
    }
  }

  void CreateEnemies()
  {
    Enemies_GO[0] = Instantiate(Enemies_SO[0].enemyPrefab, Enemies_SO[0].spawnPoint, Quaternion.identity);
  }

  void PlayerTurn()
  {
    Players_SO[whosTurn].NewRound();
    UIScript.StartPlayerTurn(Players_SO[whosTurn]);
  }

  public void OnAttackButton()
  {
    Debug.Log(Players_SO[whosTurn].Attack(Enemies_SO[0]));
  }

  public void OnMoveButton()
  {
    if(state != BattleState.PLAYERTURN)
      return;
    UIScript.DeactivatePlayerHUD();
    tileSetter.DsplMoveRange(Players_GO[whosTurn].transform.position, Players_SO[whosTurn].currentMovement);
    StartCoroutine(ValidateMovement());
  }

  IEnumerator ValidateMovement()
  {
    while(true) {
      if(Input.GetMouseButtonDown(0)) {
        // If the cursor is on the map
        Vector3 tileCoord = cursor.currentTileCoord;
        if(tileCoord.x != -1) {
          // Get distance to move
          float xDistance = tileCoord.x - Players_GO[whosTurn].transform.position.x;
          float zDistance = tileCoord.z - Players_GO[whosTurn].transform.position.z;
          float distance;
          // Absolute value
          if(xDistance < 0) {
            xDistance = xDistance * -1;
          }
          if(zDistance < 0) {
            zDistance = zDistance * -1;
          }
          if(xDistance > zDistance) {
            distance = xDistance;
          }
          else {
            distance = zDistance;
          }
          // check if that is in range
          int currentMovement = Players_SO[whosTurn].currentMovement;
          if(distance <= currentMovement) {
            // Move player
            Vector3 originPoint = (Players_GO[whosTurn].transform.position / 5f) - new Vector3(currentMovement/5 + 1, 0, currentMovement/5 + 1);
            tileSetter.ResetTexture(currentMovement*2/5 + 1, currentMovement*2/5 + 1, originPoint);
            Players_SO[whosTurn].Move(distance);
            yield return StartCoroutine(MovePlayer(new Vector3(cursor.currentTileCoord.x, 2.5f, cursor.currentTileCoord.z), 20f));
          }
        }
      }
      if(Input.GetKeyDown(KeyCode.Escape)) {
        int currentMovement = Players_SO[whosTurn].currentMovement;
        Vector3 originPoint = (Players_GO[whosTurn].transform.position / 5f) - new Vector3(currentMovement/5 + 1, 0, currentMovement/5 + 1);
        tileSetter.ResetTexture(currentMovement*2/5 + 1, currentMovement*2/5 + 1, originPoint);
        UIScript.ActivatePlayerHUD();
        yield break;
      }
      yield return null;
    }
  }

  public IEnumerator MovePlayer(Vector3 endPos, float speed)
  {
    while (Players_GO[whosTurn].transform.position != endPos)
    {
      Players_GO[whosTurn].transform.position = Vector3.MoveTowards(Players_GO[whosTurn].transform.position, endPos, speed * Time.deltaTime);
      yield return new WaitForEndOfFrame ();
    }
    UIScript.ActivatePlayerHUD();
  }

  public IEnumerator MoveEnemy(Vector3 endPos, float speed)
  {
    while (Enemies_GO[0].transform.position != endPos)
    {
      Enemies_GO[0].transform.position = Vector3.MoveTowards(Enemies_GO[0].transform.position, endPos, speed * Time.deltaTime);
      yield return new WaitForEndOfFrame ();
    }
  }

  public void OnEndTurnButton()
  {
    whosTurn++;
    if(whosTurn == Players_SO.Length) {
      state = BattleState.ENEMYTURN;
      EnemyTurn();
    }
    else if (whosTurn > Players_SO.Length) {
      whosTurn = 0;
      state = BattleState.PLAYERTURN;
      PlayerTurn();
    }
    else {
      PlayerTurn();
    }
  }

  // TODO: Either make enemy turn AI or give power over Enemy to DM
  public void EnemyTurn()
  {
    UIScript.StartEnemyTurn(Enemies_SO[0]);
    
    // Move enemy
    float factor = 5f;
    float x = Mathf.Round(Random.Range(25, 85) / factor) * factor;
    float z = Mathf.Round(Random.Range(25, 105) / factor) * factor;
    //TODO: Update enemy movement distance
    Enemies_SO[0].Move(0.0f);
    StartCoroutine(MoveEnemy(new Vector3(x, 2.5f, z), 20f));

    // Attack player
    Players_SO[(int)Mathf.Round(Random.Range(0, 5))].TakeDamage(20);

    // End enemy turn
    whosTurn = 0;
    state = BattleState.PLAYERTURN;
    PlayerTurn();
  }
}
