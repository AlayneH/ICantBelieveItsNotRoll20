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

  public void StartPlayerTurn(Character character)
  {
    PlayerHUD.SetActive(true);
    PlayerScript.SetHUD(character);
  }

  public void StartEnemyTurn(Character character)
  {
    PlayerHUD.SetActive(true);
    PlayerScript.SetHUD(character);
  }

  public void AttackPlayer(Character character)
  {
    PlayerScript.UpdateHP(character);
  }
}
