// TODO: Create an Enemy HUD for the DM
/* The UI Manager manages which state the UI is in
 * Other scripts manage the contents of those states
 * ie. Player's health change causes an event on that player's hud to update the health value
 */
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public GameObject BattleHUD;
  public GameObject PlayerHUD_PF;
  GameObject testHUD;
  List<GameObject> PlayerHUDs;


  void Start()
  {
    PlayerHUDs = new List<GameObject>();
    SetupBattle();
  }

  void SetupBattle()
  {
    BattleHUD.SetActive(true);
  }

  public GameObject SpawnPlayerHUD(int whosTurn)
  {
    GameObject PHinstance = Instantiate(PlayerHUD_PF, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    PHinstance.SetActive(false);
    PlayerHUDs.Add((GameObject)PHinstance.gameObject);
    PlayerHUDs[whosTurn].transform.SetParent(GameObject.FindGameObjectWithTag("PlayerData").transform, false);
    return PlayerHUDs[whosTurn];
  }

  public void StartPlayerTurn(int current, int previous)
  {
    PlayerHUDs[current].SetActive(true);
    PlayerHUDs[previous].SetActive(false);
  }

  // public void StartEnemyTurn(Enemy Enemy)
  // {
  //   //PlayerHUD.SetActive(true);
  //   //PlayerScript.SetHUD(Enemy.charName, Enemy.currentHP, Enemy.maxHP);
  //   PlayerHUDs[whosTurn].SetActive(true);
  // }

  // public void AttackPlayer(Player Player)
  // {
  //   PlayerScript.UpdateHP(Player.currentHP, Player.maxHP);
  // }

  public void ActivatePlayerHUD()
  {
    // MoveButton.interactable = true;
    // EndButton.interactable = true;
    // AttackButton.interactable = true;
    Debug.Log("");
  }

  public void DeactivatePlayerHUD()
  {
    // MoveButton.interactable = false;
    // EndButton.interactable = false;
    // AttackButton.interactable = false;
    Debug.Log("");
  }
}
