using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHasAbility
{
    [SerializeField]
    SerializableDictionary<string, AbilityBase> abilities = new SerializableDictionary<string, AbilityBase>();

    [SerializeField]
    StatusBase status;

    public Dictionary<string, AbilityBase> GetAbilities()
    {
        return abilities;
    }

    public StatusBase GetStatus()
    {
        return status;
    }
    protected virtual void Init()
    {
        foreach (var kvp in abilities)
        {
            kvp.Value.Initialize(gameObject);
        }
        status.Initialize(gameObject);
    }
}
