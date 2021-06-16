using System.Collections;
using System.Collections.Generic;
using UnityEngine;

﻿public class Character : MonoBehaviour
{
  public string charName;
  int maxHP;
  int currentHP;
  int AC;
  int maxMovement;
  int currentMovement;

  // public class Stats {
  //   int strength;
  //   int dexterity;
  //   int constitution;
  //   int intelligence;
  //   int wisdom;
  //   int charisma;
  // }

  public void CreateCharacter(string name, int HP, int armor, int speed)
  {
    charName = name;
    maxHP = HP;
    currentHP = maxHP;
    AC = armor;
    maxMovement = speed;
    currentMovement = maxMovement;
  }

  public void Move(Vector3 newLocation)
  {
    transform.position = newLocation;
  }

  /* Applys damage to a character
   * Returns: True if the damage knocks the character unconcious
   *          False, otherwise
   */
  public bool TakeDamage(int dmg)
  {
    currentHP = currentHP - dmg;
    if(currentHP <= 0)
      return true;
    return false;
  }
}
