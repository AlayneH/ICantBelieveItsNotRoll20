// TODO: Create an Enemy HUD for the DM
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
  public GameObject BattleHUD;
  public GameObject PlayerHUD;
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
}
