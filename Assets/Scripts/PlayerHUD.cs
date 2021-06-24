using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
  public TextMeshProUGUI nameText;
  public TextMeshProUGUI HPText;

  public void SetHUD(Character character)
  {
    nameText.text = character.charName;
    HPText.text = "HP: " + character.GetCurrentHP() + "/" + character.GetMaxHP();
  }

  public void UpdateHP(Character character)
  {
    HPText.text = "HP: " + character.GetCurrentHP() + "/" + character.GetMaxHP();
  }
}
