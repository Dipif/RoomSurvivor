using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Character : MonoBehaviour, IHasAbility
{
    [SerializeField]
    SerializableDictionary<string, AbilityBase> AbilityPrefabs = new SerializableDictionary<string, AbilityBase>();

    Dictionary<string, AbilityBase> abilities = new Dictionary<string, AbilityBase>();

    [SerializeField]
    CharacterStatus status;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var kvp in AbilityPrefabs)
        {
            var ability = Instantiate(kvp.Value, transform);
            ability.name = kvp.Key;
            ability.Initialize(this);
            abilities.Add(kvp.Key, ability);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        abilities["Move"].Activate();
    }

    public void Attack()
    {
        abilities["Attack"].Activate();
    }

    public void Stop()
    {
        status.MoveDirection = Vector3.zero;
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
