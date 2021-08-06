using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
  public TextMeshProUGUI nameText;
  public TextMeshProUGUI HPText;
  private HealthSystem healthSystem;

  public void Setup(HealthSystem healthSystem, string nameText)
  {
    this.nameText.text = nameText;
    this.healthSystem = healthSystem;
    HPText.text = "HP: " + healthSystem.GetHealth() + "/" + healthSystem.GetMaxHealth();
    healthSystem.OnHealthChanged += HS_OnHealthChanged;
  }

  private void HS_OnHealthChanged(object sender, System.EventArgs e)
  {
    HPText.text = "HP: " + healthSystem.GetHealth() + "/" + healthSystem.GetMaxHealth();
  }
}
