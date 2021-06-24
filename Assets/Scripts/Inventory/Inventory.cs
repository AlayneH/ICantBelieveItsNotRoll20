using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemAction {use, equip, drop}

public class Inventory : MonoBehaviour {
  public List<Item> items = new List<Item>();

  public void AddItem(Item item) {
    items.Add(item);
  }

  public void RemoveItem(Item item) {
    items.Remove(item);
  }
}
