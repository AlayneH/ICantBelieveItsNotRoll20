using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {weapon, armor}

public class Item : ScriptableObject {
  new public string name = "new Item";
  public ItemType type;

  public virtual void Equip() {
    // This function will be overriden
  }
}
