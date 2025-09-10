using UnityEngine;

public class EnemyStatus : StatusBase
{
    public float CurrentHealth = 100f;
    public float MaxHealth = 100f;
    public float AttackDamage = 10f;
    public float Speed = 5f;

    public FloatingTextStyle damageTextStyle;
    public override void Initialize(GameObject owner)
    {
        base.Initialize(owner);
        CurrentHealth = MaxHealth;
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        GameManager.Instance.DamageTextPool.ShowText(owner.transform.position, (int)damage, damageTextStyle);
        if (CurrentHealth <= 0f)
        {
            owner.GetComponent<Enemy>().OnDied();
        }
    }
}
