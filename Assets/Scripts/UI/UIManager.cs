// TODO: Create an Enemy HUD for the DM
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public GameObject BattleHUD;
  public GameObject PlayerHUD;
  public Button MoveButton;
  public Button EndButton;
  public Button AttackButton;
  public PlayerHUD PlayerScript;

  void Start()
  {
    SetupBattle();
  }

  void SetupBattle()
  {
    BattleHUD.SetActive(true);
    PlayerScript = PlayerHUD.GetComponent<PlayerHUD>();
  }

  public void StartPlayerTurn(Player Player)
  {
    PlayerHUD.SetActive(true);
    PlayerScript.SetHUD(Player.charName, Player.currentHP, Player.maxHP);
    MoveButton.interactable = true;
  }

  public void StartEnemyTurn(Enemy Enemy)
  {
    PlayerHUD.SetActive(true);
    PlayerScript.SetHUD(Enemy.charName, Enemy.currentHP, Enemy.maxHP);
  }

  public void AttackPlayer(Player Player)
  {
    PlayerScript.UpdateHP(Player.currentHP, Player.maxHP);
  }

  public void ActivatePlayerHUD()
  {
    MoveButton.interactable = true;
    EndButton.interactable = true;
    AttackButton.interactable = true;
  }

  public void DeactivatePlayerHUD()
  {
    MoveButton.interactable = false;
    EndButton.interactable = false;
    AttackButton.interactable = false;
  }
}
