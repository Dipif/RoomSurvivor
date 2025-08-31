using UnityEngine;

public class SuperPunchAbility : AbilityBase
{
    [SerializeField]
    GameObject SuperPunchPrefab;

    public float AttackArea = 1.0f;

    public override void Activate()
    {
    }

    public override void Deactivate()
    {
    }

    public override void OnEvent(string eventName)
    {
    }
}
