// TODO: Update returned values in GameManager to reflect the return values in TakeDamage and Move
// TODO: Add a character class for Player and Enemy overlap
  // Attack parameter will then be any character so that players can attack players as well
  // Not urgent, but it will help with coding convention and possible expansions
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName =  "new Player", menuName = "Player")]

public class Player : ScriptableObject
{
  public string charName;
  public int maxHP;
  public int currentHP;
  public int AC;
  public int maxMovement;
  public int currentMovement;

  // TODO: Either make this the small numbers or find a way to correctly do values < 10
  public int strength;
  public int dexterity;
  public int constitution;
  public int intelligence;
  public int wisdom;
  public int charisma;

  public int profBonus;

  public Vector3 spawnPoint;
  public GameObject playerPrefab;
  public List<Item> inventory;

  /* Applys damage to this player
   * Returns: True if the damage knocks the player unconcious
   *          False, otherwise
   */
  public bool TakeDamage(int dmg)
  {
    currentHP = currentHP - dmg;
    if(currentHP <= 0)
      return true;
    return false;
  }

  /* Adjusts leftover movement for the player
   * Returns: True if the player is now out of movement
   *          False, otherwise
   */
  public bool Move(float distance)
  {
    currentMovement = currentMovement - (int)distance;
    if(currentMovement == 0)
      return true;
    return false;
  }

  public int Action()
  {
    // What action?
      // Options: Attack, Dash, Other, Cancel/GoBack
    // If action is taken, disable action button
    /* NOTE: Exceptions to this may exist within things like extra attack */
    return ToHit();
    // Using shortsword
    // Roll To Hit?
  }

  public int Attack(Enemy targetEnemy)
  {
    int atkRoll = ToHit();
    int damage = 0;
    if(targetEnemy.CheckHit(atkRoll))
    {
      damage = inventory[0].Use() + (dexterity-10)/2;
      targetEnemy.TakeDamage(damage);
    }
    return damage;
    // TODO: else visualy indicate the attack missed and continue
  }

  // TODO: Implement adv and disadv
  // TODO: Add bonus based on weapon used ie. +1 sword
  private int ToHit()
  {
    System.Random r = new System.Random();
    // roll d20
    int total = r.Next(1, 21);
    // add modifiers
    total = total + profBonus + (dexterity-10)/2;
    return total;
  }

  public void NewRound()
  {
    currentMovement = maxMovement;
  }
}
