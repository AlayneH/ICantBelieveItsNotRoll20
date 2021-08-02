using System.Collections;
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

  public int strength;
  public int dexterity;
  public int constitution;
  public int intelligence;
  public int wisdom;
  public int charisma;

  public Vector3 spawnPoint;
  public GameObject playerPrefab;

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

  public void NewRound()
  {
    currentMovement = maxMovement;
  }
}
