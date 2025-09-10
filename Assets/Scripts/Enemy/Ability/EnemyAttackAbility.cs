using UnityEngine;

public class EnemyAttackAbility : AbilityBase
{

    public override void Initialize(GameObject owner)
    {
        base.Initialize(owner);
        remainingCooldown = 0f;
    }
    public override void Activate()
    {
        GameManager.Instance.Player.gameObject.GetComponent<KnockbackController>().ApplyFromSource(owner.transform.position, 1.0f, 0.2f);
        IHasAbility playerHasAbility = GameManager.Instance.Player.GetComponent<IHasAbility>();
        CharacterStatus playerStatus = playerHasAbility.GetStatus() as CharacterStatus;
        playerStatus.TakeDamage(1);
    }

    public override bool CanActivate()
    {
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 enemyPos = owner.transform.position;
        float distance = Vector3.Distance(playerPos, enemyPos);
        return distance <= 0.5f;
    }
}
