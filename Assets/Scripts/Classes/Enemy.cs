// TODO: Either change move to be a path instead of a point, or implement pathfinding here
// Preferably the first option so that the charcter doesn't know information about the map

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName =  "new Enemy", menuName = "Enemy")]

﻿public class Enemy : ScriptableObject
{
  public string charName;
  public int maxHP;
  public int currentHP;
  public int AC;
  public int maxMovement;
  public int currentMovement;

  // public class Stats {
  //   int strength;
  //   int dexterity;
  //   int constitution;
  //   int intelligence;
  //   int wisdom;
  //   int charisma;
  // }
  public Vector3 spawnPoint;
  public GameObject enemyPrefab;
  // Inventory

  /* Adjusts leftover movement for the enemy
   * Returns: True if the enemy is now out of movement
   *          False, otherwise
   */
   public bool Move(float distance)
   {
     currentMovement = currentMovement - (int)distance;
     if(currentMovement == 0)
       return true;
     return false;
   }

  /* Applys damage to this enemy
   * Returns: True if the damage knocks the enemy unconcious
   *          False, otherwise
   */
  public bool TakeDamage(int dmg)
  {
    currentHP = currentHP - dmg;
    if(currentHP <= 0)
      return true;
    return false;
  }

  public void NewRound()
  {
    currentMovement = maxMovement;
  }
}
