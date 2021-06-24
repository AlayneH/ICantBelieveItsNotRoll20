using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName =  "new Ranged Weapon", menuName = "Items/Weapons/Ranged")]

public class Ranged : Weapon {
  public int ammoCount;
  public int disadvRange;
  public bool loading;
}
