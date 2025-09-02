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

    public float AttackAreaModifier = 1.0f;

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
        if (eventName == "SuperPunchHit")
        {
            Vector3 effectPosition = fist.transform.position;
            effectPosition.y = 1f;
            Instantiate(effect, effectPosition, Quaternion.identity);
            attackArea.transform.position = effectPosition;

            Collider[] hitTargets = Physics.OverlapCapsule(
                attackArea.transform.TransformPoint(attackArea.center),
                attackArea.transform.TransformPoint(attackArea.center + Vector3.up * attackArea.height * 0.5f),
                attackArea.radius * AttackAreaModifier
                );
            
            foreach (var target in hitTargets)
            {
                if (target.gameObject == owner)
                    continue;

                if (target.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    ((EnemyStatus)enemy.GetStatus()).TakeDamage(10);
                }
                if (target.TryGetComponent<Wall>(out Wall wall))
                {
                    ((WallStatus)wall.GetStatus()).TakeDamage(10);
                }
            }
        }
        else if (eventName == "SuperPunchEnd")
        {
            fist.gameObject.SetActive(false);
        }
    }
}