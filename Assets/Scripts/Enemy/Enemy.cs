using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHasAbility
{
    [SerializeField]
    SerializableDictionary<string, AbilityBase> AbilityPrefabs = new SerializableDictionary<string, AbilityBase>();

    Dictionary<string, AbilityBase> abilities = new Dictionary<string, AbilityBase>();

    [SerializeField]
    CharacterStatus status;

    public Dictionary<string, AbilityBase> GetAbilities()
    {
        return abilities;
    }

    public StatusBase GetStatus()
    {
        return status;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
