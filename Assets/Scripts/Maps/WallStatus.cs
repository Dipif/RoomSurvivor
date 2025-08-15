using UnityEngine;

public class WallStatus : StatusBase
{
    public float CurrentHealth = 10f;
    public float MaxHealth = 10f;

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0f)
        {
            Destroy(owner);
        }
    }
}
