// TODO: Make sure to account for ammo
// The player can hold multiple weapons with the same type of ammo
// So ammo is an item that the player has

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName =  "new Ranged Weapon", menuName = "Items/Weapons/Ranged")]

public class Ranged : Weapon {
  public int disadvRange;
  public bool loading;
}
