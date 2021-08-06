using System;

public class HealthSystem
{
  public event EventHandler OnHealthChanged;

  private int health;
  private int healthMax;

  public HealthSystem()
  {
    healthMax = 100;
    health = healthMax;
  }

  public HealthSystem(int healthMax)
  {
    this.healthMax = healthMax;
    health = healthMax;
  }

  public void ChangeHealthMax(int healthMax, bool heal)
  {
    this.healthMax = healthMax;
    if(heal)
      health = healthMax;
    else if (health > healthMax)
      health = healthMax;
    if(OnHealthChanged != null)
      OnHealthChanged(this, EventArgs.Empty);
  }

  public int GetHealth()
  {
    return health;
  }

  public int GetMaxHealth()
  {
    return healthMax;
  }

  public float GetHealthPercent()
  {
    return (float)health/healthMax;
  }

  public void Damage(int dmgAmount)
  {
    health -= dmgAmount;
    if (health < 0)
      health = 0;
    if(OnHealthChanged != null)
      OnHealthChanged(this, EventArgs.Empty);
  }

  public void Heal(int healAmount)
  {
    health += healAmount;
    if(health > healthMax)
      health = healthMax;
    if(OnHealthChanged != null)
      OnHealthChanged(this, EventArgs.Empty);
  }
}
