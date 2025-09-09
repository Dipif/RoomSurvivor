using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameRestartEventChannel", menuName = "Scriptable Objects/GameRestartEventChannel")]
public class GameRestartEventChannel : ScriptableObject
{
    public event Action OnRaised;
    public void Raise() => OnRaised?.Invoke();
}
