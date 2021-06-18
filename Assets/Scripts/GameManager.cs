// TODO: Make visual range of movement use character values, not hard coded numbers
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, END}
public enum PlayerNames {BLANCHE, FELEMAR, LUCIAN, RUFFLES, SULLY}

public class GameManager : MonoBehaviour
{
  public GameObject mapPrefab;
  public GameObject playerPrefab;
  public GameObject enemyPrefab;
  public Transform blancheLocation;
  public Transform felemarLocation;
  public Transform lucianLocation;
  public Transform rufflesLocation;
  public Transform sullyLocation;
  public Transform enemyLocation;
  public BattleState state;
  public BattleHUD playerHUD;

  PlayerInfo[] Players = new PlayerInfo[5]; // player index is in alphabetical order for now
  // Blanche, Felemar, Lucian, Ruffles, Sully
  // TODO: change this order to be based on initiative rolls
  TileMapMouse cursor;
  GameObject Enemy;
  Character EnemyStats;
  GameObject map;
  TGMap tileSetter;
  int whosTurn = 0;

  class PlayerInfo
  {
    public GameObject Player;
    public Character PlayerStats;
  }

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
    // Blanche
    Players[0] = new PlayerInfo();
    Players[0].Player = Instantiate(playerPrefab, blancheLocation);
    Players[0].PlayerStats = Players[0].Player.GetComponent<Character>();
    Players[0].PlayerStats.CreateCharacter("Blanche", 80, 17, 30);
    // Felemar
    Players[1] = new PlayerInfo();
    Players[1].Player = Instantiate(playerPrefab, felemarLocation);
    Players[1].PlayerStats = Players[1].Player.GetComponent<Character>();
    Players[1].PlayerStats.CreateCharacter("Felemar", 80, 17, 35);
    // Lucian
    Players[2] = new PlayerInfo();
    Players[2].Player = Instantiate(playerPrefab, lucianLocation);
    Players[2].PlayerStats = Players[2].Player.GetComponent<Character>();
    Players[2].PlayerStats.CreateCharacter("Lucian", 80, 17, 30);
    // Ruffles
    Players[3] = new PlayerInfo();
    Players[3].Player = Instantiate(playerPrefab, rufflesLocation);
    Players[3].PlayerStats = Players[3].Player.GetComponent<Character>();
    Players[3].PlayerStats.CreateCharacter("Ruffles", 80, 17, 30);
    // Sully
    Players[4] = new PlayerInfo();
    Players[4].Player = Instantiate(playerPrefab, sullyLocation);
    Players[4].PlayerStats = Players[4].Player.GetComponent<Character>();
    Players[4].PlayerStats.CreateCharacter("Sully", 80, 17, 30);
  }

  void CreateEnemies()
  {
    Enemy = Instantiate(enemyPrefab, enemyLocation);
    EnemyStats = Enemy.GetComponent<Character>();
    EnemyStats.CreateCharacter("Enemy", 80, 17, 30);
  }

  void PlayerTurn()
  {
    playerHUD.SetHUD(Players[whosTurn].PlayerStats);
    Players[whosTurn].PlayerStats.NewRound();
  }

  public void OnMoveButton()
  {
    if(state != BattleState.PLAYERTURN)
      return;
        // Origin point is the bottom left corner of the range
    int currentMovement = Players[whosTurn].PlayerStats.GetCurrentMovement();
    Vector3 originPoint = (Players[whosTurn].Player.transform.position / 5f) - new Vector3(currentMovement/5 + 1, 0, currentMovement/5 + 1);
    tileSetter.SetTexture(currentMovement*2/5 + 1, currentMovement*2/5 + 1, 0, originPoint);
    StartCoroutine(PlayerMove());
  }

  IEnumerator PlayerMove()
  {
    while(true) {
      if(Input.GetMouseButtonDown(0)) {
        // If the cursor is on the map
        Vector3 tileCoord = cursor.currentTileCoord;
        if(tileCoord.x != -1)
        {
          // Get distance to move
          float xDistance = tileCoord.x - Players[whosTurn].Player.transform.position.x;
          float zDistance = tileCoord.z - Players[whosTurn].Player.transform.position.z;
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
          int currentMovement = Players[whosTurn].PlayerStats.GetCurrentMovement();
          if(distance <= currentMovement) {
            // Move player
            Vector3 originPoint = (Players[whosTurn].Player.transform.position / 5f) - new Vector3(currentMovement/5 + 1, 0, currentMovement/5 + 1);
            tileSetter.ResetTexture(currentMovement*2/5 + 1, currentMovement*2/5 + 1, originPoint);
            Players[whosTurn].PlayerStats.Move(new Vector3(cursor.currentTileCoord.x, 2.5f, cursor.currentTileCoord.z), distance);
            yield break;
          }
        }
      }
      yield return null;
    }
  }

  public void OnEndTurnButton()
  {
    whosTurn++;
    if(whosTurn == 5)
    {
      state = BattleState.ENEMYTURN;
      StartCoroutine(EnemyTurn());
    }
    else if (whosTurn == 6)
    {
      whosTurn = 0;
      state = BattleState.PLAYERTURN;
      PlayerTurn();
    }
    else
    {
      PlayerTurn();
    }
  }

  IEnumerator EnemyTurn()
  {
    playerHUD.SetHUD(EnemyStats);
    yield return new WaitForSeconds(1);

    // Move enemy
    float factor = 5f;
    float x = Mathf.Round(Random.Range(25, 85) / factor) * factor;
    float z = Mathf.Round(Random.Range(25, 105) / factor) * factor;
    Enemy.transform.position = new Vector3(x, 2.5f, z);
    yield return new WaitForSeconds(1);

    // End enemy turn
    whosTurn = 0;
    state = BattleState.PLAYERTURN;
    PlayerTurn();
    yield return null;
  }
}
