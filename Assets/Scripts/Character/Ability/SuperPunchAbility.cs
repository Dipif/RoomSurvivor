using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SuperPunchAbility : AbilityBase
{
    [SerializeField]
    GameObject fistPrefab;

    [SerializeField]
    CapsuleCollider attackArea;

    [SerializeField]
    float attackHeight = 2.0f;

    [SerializeField]
    GameObject effect;

    SuperPunchFist fist;

    public override void Activate()
    {
        if (fist == null)
        {
            var fistObject = Instantiate(fistPrefab);
            fist = fistObject.GetComponent<SuperPunchFist>();
            fist.Init(owner.GetComponent<IHasAbility>());
            fist.gameObject.SetActive(false);
        }

        Vector2 randomCircle = Random.insideUnitCircle * 2f;
        Vector3 randomPosition = new Vector3(
            transform.position.x + randomCircle.x,
            attackHeight,
            transform.position.z + randomCircle.y
        );

        fist.transform.position = randomPosition;

        fist.gameObject.SetActive(true);
        fist.Play("SuperPunch");
    }

    public override void Deactivate()
    {
        fist.gameObject.SetActive(false);
        fist.Play("Idle");
    }

    public override void OnEvent(string eventName)
    {
        IHasAbility hasAbility = owner.GetComponent<IHasAbility>();
        CharacterStatus status = (CharacterStatus)hasAbility.GetStatus();
        switch (eventName)
        {
            case "SuperPunchHit":
                Vector3 effectPosition = fist.transform.position;
                effectPosition.y = 1f;
                Instantiate(effect, effectPosition, Quaternion.identity);
                attackArea.transform.position = effectPosition;

                Collider[] hitTargets = Physics.OverlapCapsule(
                    attackArea.transform.TransformPoint(attackArea.center),
                    attackArea.transform.TransformPoint(attackArea.center + Vector3.up * attackArea.height * 0.5f),
                    attackArea.radius
                    );

                foreach (var target in hitTargets)
                {
                    if (target.gameObject == owner)
                        continue;

                    if (target.TryGetComponent<Enemy>(out Enemy enemy))
                    {
                        ((EnemyStatus)enemy.GetStatus()).TakeDamage(10 * status.SuperPunchDamageMultiplier);
                    }
                    if (target.TryGetComponent<Wall>(out Wall wall))
                    {
                        ((WallStatus)wall.GetStatus()).TakeDamage(10 * status.SuperPunchDamageMultiplier);
                    }
                }
                break;
            case "SuperPunchEnd":
                fist.gameObject.SetActive(false);
                break;
            case "ModifySuperPunchCooldown":
                cooldown = baseCooldown * status.SuperPunchCooldownMultiplier;
                break;

        }
    }
}