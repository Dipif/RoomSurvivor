using System.Collections.Generic;
using UnityEngine;

public enum CostType
{
    Manual,
    Linear
}

[CreateAssetMenu(fileName = "UpgradeOption", menuName = "Scriptable Objects/UpgradeOption")]
public class UpgradeOption : ScriptableObject
{
    public int Level;
    public string Title;
    public string Description;
    public UpgradeEffect Effect;

    [SerializeField, HideInInspector] private CostType costType = CostType.Manual;

    [SerializeField, HideInInspector] private List<int> manualCosts = new();
    [SerializeField, HideInInspector] private int linearStartCost = 100;
    [SerializeField, HideInInspector] private int linearIncrement = 50;

    public int GetCost(int level)
    {
        switch (costType)
        {
            case CostType.Manual:
                if (level < manualCosts.Count)
                    return manualCosts[level];
                else
                    return manualCosts.Count > 0 ? manualCosts[manualCosts.Count - 1] : 0;
            case CostType.Linear:
                return linearStartCost + linearIncrement * level;
            default:
                return 0;
        }
    }

    public void ApplyTo(GameObject target)
    {
        Level++;
        Effect.ApplyTo(GameManager.Instance.Player.gameObject);
    }
}
