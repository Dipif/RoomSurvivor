using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHasAbility
{
    [SerializeField]
    SerializableDictionary<string, AbilityBase> abilities = new SerializableDictionary<string, AbilityBase>();

    public CharacterStatus status;

    void Start()
    {
        foreach (var kvp in abilities)
        {
            var ability = kvp.Value;
            ability.Initialize(gameObject);
        }
        status.Initialize(gameObject);
    }

    void FixedUpdate()
    {
        abilities["Move"].Activate();
    }

    public void Attack()
    {
        abilities["Attack"].Activate();
    }

    public void OnAttackPerform()
    {
        abilities["Attack"].OnEvent("Perform");
    }

    public void Stop()
    {
        abilities["Move"].Deactivate();
    }

    public void SetDirection(Vector3 direction)
    {
        status.MoveDirection = direction.normalized;
    }

    public Dictionary<string, AbilityBase> GetAbilities()
    {
        return abilities;
    }

    public StatusBase GetStatus()
    {
        return status;
    }
}
