using System.Collections.Generic;
using UnityEngine;

public interface IHasAbility
{
    Dictionary<string, AbilityBase> GetAbilities();
    StatusBase GetStatus();
}
