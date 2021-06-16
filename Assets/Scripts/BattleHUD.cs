using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleHUD : MonoBehaviour
{
  public TextMeshProUGUI nameText;
  public GameObject moveButton;

  public void SetHUD(Character character)
  {
    nameText.text = character.charName;
    Debug.Log(character.charName);
  }

  public void SetHP(int hp)
  {
    // reflect hp on UI to be hp
  }

  // public void HideHUD()
  // {
  //   nameText.SetActive(false);
  //   moveButton.SetActive(false);
  // }
  //
  // public void ShowHUD()
  // {
  //   nameText.SetActive(true);
  //   moveButton.SetActive(true);
  // }
}
