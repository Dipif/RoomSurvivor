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

    public void OnAbilityEvent(string eventName)
    {
        throw new System.NotImplementedException();
    }

    public virtual void Init()
    {
        foreach (var kvp in abilities)
        {
            kvp.Value.Initialize(gameObject);
        }
        status.Initialize(gameObject);
    }

    public virtual void OnDied()
    {
        var currentRoom = GameManager.Instance.GetCurrentRoom();
        GameManager.Instance.Gold += 1;
        GameManager.Instance.Score += 1;
        gameObject.SetActive(false);
    }
}