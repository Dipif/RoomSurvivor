using UnityEngine;

public class SuperPunchAbility : AbilityBase
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    GameObject fist;

    public float AttackArea = 1.0f;

    public override void Activate()
    {
        fist.SetActive(true);
        animator.Play("SuperPunch");
    }

    public override void Deactivate()
    {
    }

    public override void OnEvent(string eventName)
    {
        if (eventName == "SuperPunchHit")
        {
        }
    }
}
