using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName =  "new TM Weapon", menuName = "Items/Weapons/Melee/Thrown")]

public class Thrown : Melee {
  public int throwRange;
  public int disadvRange;

  /* Thrown attack */
  // Item is now on the field
  // Dex mod to hit
  // Calculate damage in theory, is the same as the normal melee weapon's damage calculation
}
