using System.Net.NetworkInformation;
using UnityEngine;

public class CharacterAttackAbility : AbilityBase
{
    Animator animator;

    [SerializeField]
    Transform attackPoint;

    public float AttackArea = 1.0f;
    public override void Initialize(GameObject owner)
    {
        base.Initialize(owner);

        animator = owner.GetComponentInChildren<Animator>();
    }

    public override void Activate()
    {
        animator.SetBool("IsMoving", false);
        IHasAbility hasAbility = owner.GetComponent<IHasAbility>();
        CharacterStatus status = (CharacterStatus)hasAbility.GetStatus();
        status.MoveLockTimer = 0f;
        status.IsMoveLock = true;
        animator.Play("Attack");
    }

    public override void Deactivate()
    {
        IHasAbility hasAbility = owner.GetComponent<IHasAbility>();
        CharacterStatus status = (CharacterStatus)hasAbility.GetStatus();
        status.IsMoveLock = false;
        status.MoveLockTimer = 0f;
        animator.SetBool("IsMoving", false);
        animator.Play("Idle");
    }

    public override void OnEvent(string eventName)
    {
        if (eventName == "Perform")
        {
            IHasAbility hasAbility = owner.GetComponent<IHasAbility>();
            CharacterStatus status = (CharacterStatus)hasAbility.GetStatus();

            Vector3 attackDirection = owner.transform.forward;
            Vector3 attackPosition = attackPoint.position;

            // draw box for debugging
            Debug.DrawLine(attackPosition + AttackArea * Vector3.down, attackPosition + AttackArea * Vector3.up, Color.red, 1.0f);
            Debug.DrawLine(attackPosition - AttackArea * attackDirection, attackPosition + AttackArea * attackDirection, Color.red, 1.0f);


            Collider[] hitTargets = Physics.OverlapBox(attackPosition, AttackArea * new Vector3(1.0f, 1.0f, 1.0f), Quaternion.identity);
            foreach (var target in hitTargets)
            {
                if (target.gameObject == owner)
                    continue;

                // target gameobject가 Enemy인지 확인
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
    }
}
