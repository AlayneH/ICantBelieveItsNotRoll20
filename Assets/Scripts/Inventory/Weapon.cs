using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType {none, acid, bludgeoning, cold, fire, force, lightning, necrotic, piercing, poison, psychic, radiant, slashing, thunder}

[CreateAssetMenu (fileName =  "new Weapon", menuName = "Items/Weapons")]

﻿public class Weapon: Item {
  public int range;
  public int atkMod;
  public int dmgMod;
  public int diceType; // 4, 6, 8, 10, 12, 20
  public int numDice;
  public DamageType dmgType;

  public override void Equip() {

  }

  // Calculate Damage
  public int CalcDamage() {
    int totalDamage = 0;
    // Roll dice
    System.Random r = new System.Random();
    for(int i = 1; i <= numDice; i++) {
      totalDamage = totalDamage + r.Next(1, diceType);
    }
    // Add modifiers
    totalDamage = totalDamage + dmgMod;
    return totalDamage;
  }

  public int GetRange() {
    return range;
  }

  public int GetAtkMod() {
    return atkMod;
  }

  public DamageType GetDmgType() {
    return dmgType;
  }
}
