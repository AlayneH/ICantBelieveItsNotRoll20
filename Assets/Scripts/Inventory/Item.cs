using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {weapon, armor}
[CreateAssetMenu (fileName =  "new Item", menuName = "Item")]

public class Item : ScriptableObject {
  new public string name = "new Item";
  public ItemType type;

  // This function may fit better in an inventory or player script
  public virtual void Equip() {
    // This function may be overridden
  }

  public virtual void Use() {
    // This function will be overriden
  }

  public virtual void Drop() {
    // This function can be overridden if further functionality is needed
  }
}
