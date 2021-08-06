// TODO: Remove Players_GO from this file and keep it as the token in Players_SO
// TODO: Look into which is better map.GetComponent<TDMap>() or tileSetter.OccupySpace()
// TODO: Character's currently load faster than the map, which is fine, but if I ever implement gravity thats a problem
  // This also means that the tile occupant can't be set as character's spawn, so that value is currently hard coded in
using System;
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
  public GameObject map;
  TGMap tileSetter;
  int whosTurn = 0;

  void Start()
  {
    state = BattleState.START;
    SetupBattle();
  }

  // update and check state every frame to have correct response
  void SetupBattle()
  {
    CreateMap();
    UIScript = UIManager.GetComponent<UIManager>();
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
    for(int i = 0; i < 5; i++)
    {
      // Spawn Player
      Vector3 spawnPoint = Players_SO[i].spawnPoint;
      int x = (int)(spawnPoint.x-5/5);
      int z = (int)(spawnPoint.z-5/5);
      Players_GO[i] = Instantiate(Players_SO[i].playerPrefab, Players_SO[i].spawnPoint, Quaternion.identity);
      Players_SO[i].token = Players_GO[i];
      // Set up HealthSystem and HUD
      GameObject hudInstance = UIScript.SpawnPlayerHUD(i);
      Players_SO[i].Setup(new HealthSystem(), hudInstance.GetComponent<PlayerHUD>());
    }
    Debug.Log("Players Created");
  }

  void CreateEnemies()
  {
    Enemies_GO[0] = Instantiate(Enemies_SO[0].enemyPrefab, Enemies_SO[0].spawnPoint, Quaternion.identity);
  }

  void PlayerTurn()
  {
    Players_SO[whosTurn].NewRound();
    Debug.Log(Players_SO[whosTurn].charName + "'s turn");
    int previous = whosTurn-1;
    if(previous < 0)
      previous = 4;
    UIScript.StartPlayerTurn(whosTurn, previous);
  }

  public void OnAttackButton()
  {
    if(state != BattleState.PLAYERTURN)
      return;
    UIScript.DeactivatePlayerHUD();
    // Get weapon range
    int range = Players_SO[whosTurn].GetAtkRange();
    // Display Range
    tileSetter.DsplRange(Players_SO[whosTurn].token.transform.position, range, 1);
    // Select Enemy
    cursor.Select(() => {
      Vector3 tileCoord = cursor.selectedTileCoord;
      // if an enemy was selected
      if(map.GetComponent<TDMap>().GetOccupant((int)((tileCoord.x-5)/5), (int)((tileCoord.z-5)/5)) == 2) {
        // Attack enemy
        int damage = Players_SO[whosTurn].Attack(Enemies_SO[0]);
        Debug.Log(damage);
      }
      // if selection is in range

      tileSetter.ResetTexture(range, range, Players_SO[whosTurn].token.transform.position);
      UIScript.ActivatePlayerHUD();
    });
  }

  public void OnMoveButton()
  {
    Debug.Log("Move Clicked");
    if(state != BattleState.PLAYERTURN)
      return;
    UIScript.DeactivatePlayerHUD();
    Vector3 curLocation = Players_GO[whosTurn].transform.position;
    // Debug.Log(map.GetComponent<TDMap>().GetOccupant(5, 5));
    // map.GetComponent<TDMap>().SetOccupant((int)((curLocation.x-5)/5), (int)((curLocation.z-5)/5), 0);

    tileSetter.DsplRange(curLocation, Players_SO[whosTurn].currentMovement, 0);
    cursor.Select(() => {
      map.GetComponent<TDMap>().SetOccupant((int)((curLocation.x-5)/5), (int)((curLocation.z-5)/5), 0);
      Vector3 tileCoord = cursor.selectedTileCoord;
      int distance = ValidateMovement(tileCoord);
      int currentMovement = Players_SO[whosTurn].currentMovement;
      Vector3 originPoint = (curLocation / 5f) - new Vector3(currentMovement/5 + 1, 0, currentMovement/5 + 1);
      tileSetter.ResetTexture(currentMovement*2/5 + 1, currentMovement*2/5 + 1, originPoint);
      if(distance != -1)
        Players_SO[whosTurn].Move(new Vector3(tileCoord.x, 2.5f, tileCoord.z), distance);
      UIScript.ActivatePlayerHUD();
      map.GetComponent<TDMap>().SetOccupant((int)((tileCoord.x-5)/5), (int)((tileCoord.z-5)/5), 1);
    });
  }

  public int ValidateMovement(Vector3 tileCoord)
  {
    float xDistance = tileCoord.x - Players_GO[whosTurn].transform.position.x;
    float zDistance = tileCoord.z - Players_GO[whosTurn].transform.position.z;
    float distance;
    // Absolute value
    if(xDistance < 0)
      xDistance = xDistance * -1;
    if(zDistance < 0)
      zDistance = zDistance * -1;
    // Distance = furthest direction, x or z
    if(xDistance > zDistance)
      distance = xDistance;
    else
      distance = zDistance;
    // check if that is in range for player speed
    int currentMovement = Players_SO[whosTurn].currentMovement;
    if(distance <= currentMovement)
      return (int)distance;
    return -1;
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
    //UIScript.StartEnemyTurn(Enemies_SO[0]);

    // Move enemy
    float factor = 5f;
    float x = Mathf.Round(UnityEngine.Random.Range(25, 85) / factor) * factor;
    float z = Mathf.Round(UnityEngine.Random.Range(25, 105) / factor) * factor;
    //TODO: Update enemy movement distance
    Enemies_SO[0].Move(0.0f);
    StartCoroutine(MoveEnemy(new Vector3(x, 2.5f, z), 20f));

    // Attack player
    Players_SO[(int)Mathf.Round(UnityEngine.Random.Range(0, 5))].TakeDamage(20);

    // End enemy turn
    whosTurn = 0;
    state = BattleState.PLAYERTURN;
    PlayerTurn();
  }
}
