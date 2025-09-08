using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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

    public void Attack()
    {
        abilities["Attack"].Activate();
    }

    public void OnAbilityEvent(string eventName)
    {
        foreach (var ability in abilities.Values)
        {
            ability.OnEvent(eventName);
        }
    }

    public void Stop()
    {
        abilities["Move"].Deactivate();
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
