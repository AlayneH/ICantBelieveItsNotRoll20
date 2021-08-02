using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName =  "new VM Weapon", menuName = "Items/Weapons/Melee/Versatile")]

public class Versatile : Melee {
  public int vDiceType;
  public int vNumDice;

  public int VersatileDamage() {
    int totalDamage = 0;
    // Roll dice
    System.Random r = new System.Random();
    for(int i = 1; i <= vNumDice; i++) {
      totalDamage = totalDamage + r.Next(1, vDiceType);
    }
    // Add modifiers
    totalDamage = totalDamage + dmgMod;
    return totalDamage;

    // TODO: If applicable, note that the palyer is using two hands for this
    // Some turn options will become unavailable when this is used
  }
}
