using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHasAbility
{
    [SerializeField]
    SerializableDictionary<string, AbilityBase> AbilityPrefabs = new SerializableDictionary<string, AbilityBase>();

    Dictionary<string, AbilityBase> abilities = new Dictionary<string, AbilityBase>();

    public CharacterStatus status;

    void Start()
    {
        foreach (var kvp in AbilityPrefabs)
        {
            var ability = Instantiate(kvp.Value, transform);
            ability.name = kvp.Key;
            ability.Initialize(gameObject);
            abilities.Add(kvp.Key, ability);
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
