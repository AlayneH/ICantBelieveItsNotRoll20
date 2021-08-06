using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
  private HealthSystem healthSystem;

  public void Setup(HealthSystem healthSystem)
  {
    this.healthSystem = healthSystem;

    healthSystem.OnHealthChanged += HS_OnHealthChanged;
  }

  private void HS_OnHealthChanged(object sender, System.EventArgs e)
  {
    Debug.Log("health: " + healthSystem.GetHealth());
    Debug.Log("percent " + healthSystem.GetHealthPercent());
    transform.Find("Bar").localScale = new Vector3(healthSystem.GetHealthPercent(), 1);
  }
}
