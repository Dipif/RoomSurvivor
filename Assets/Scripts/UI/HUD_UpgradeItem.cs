using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD_UpgradeItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI costText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] UpgradeOption upgradeOption;

    public void Init(UpgradeOption option, System.Action onClick)
    {
        upgradeOption = option;
        if (descriptionText != null)
        {
            descriptionText.text = option.Title + "\n" + option.Description;
        }
        if (costText != null)
        {
            int cost = option.GetCost(option.Level);
            costText.text = cost.ToString();
        }
        if (levelText != null)
        {
            levelText.text = option.Level.ToString();
        }
    }
    public void OnClick()
    {
        int cost = upgradeOption.GetCost(upgradeOption.Level);
        if (GameManager.Instance.Gold < cost)
        {
            return;
        }
        GameManager.Instance.Gold -= cost;
        upgradeOption.ApplyTo(GameManager.Instance.Player.gameObject);
    }
}
