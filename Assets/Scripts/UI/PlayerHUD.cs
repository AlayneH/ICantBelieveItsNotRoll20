using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
  public TextMeshProUGUI nameText;
  public TextMeshProUGUI HPText;

  public void SetHUD(string charName, int currentHP, int maxHP)
  {
    nameText.text = charName;
    UpdateHP(currentHP, maxHP);
  }

  public void UpdateHP(int currentHP, int maxHP)
  {
    HPText.text = "HP: " + currentHP + "/" + maxHP;
  }
}
