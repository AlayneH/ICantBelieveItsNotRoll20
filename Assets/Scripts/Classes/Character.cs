// TODO: Either change move to be a path instead of a point, or implement pathfinding here
// Preferably the first option so that the charcter doesn't know information about the map

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

  public void Move(Vector3 endPos, float distance)
  {
    currentMovement = currentMovement - (int)distance;
    StartCoroutine(MoveOverSpeed(endPos, 20f));
  }

  public IEnumerator MoveOverSpeed(Vector3 endPos, float speed)
  {
    while (transform.position != endPos)
    {
      transform.position = Vector3.MoveTowards(transform.position, endPos, speed * Time.deltaTime);
      yield return new WaitForEndOfFrame ();
    }
  }

  public int GetCurrentMovement() {
    return currentMovement;
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

  public void NewRound()
  {
    currentMovement = maxMovement;
  }
}
