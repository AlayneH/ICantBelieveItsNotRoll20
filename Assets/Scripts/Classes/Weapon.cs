using System.Collections;
using System.Collections.Generic;
using UnityEngine;

﻿public class Weapon : MonoBehaviour
{
  int range;
  int dmgMod;
  int atkMod;

  public Weapon(int range, int dmgMod, int atkMod) {
    this.range = range;
    this.dmgMod = dmgMod;
    this.atkMod = atkMod;
  }
}
