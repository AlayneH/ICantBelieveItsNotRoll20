using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
  public TextMeshProUGUI nameText;

  public void SetHUD(Character character)
  {
    nameText.text = character.charName;
  }
}
