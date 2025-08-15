using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IHasAbility
{
    Dictionary<string, AbilityBase> abilities = new Dictionary<string, AbilityBase>();

    public WallStatus status;

    void Start()
    {
        status.Initialize(gameObject);
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
