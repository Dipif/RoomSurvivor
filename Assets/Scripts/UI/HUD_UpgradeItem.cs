using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD_UpgradeItem : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI descriptionText;

    [SerializeField]
    UpgradeOption upgradeOption;

    public void Init(UpgradeOption option, System.Action onClick)
    {
        upgradeOption = option;
        if (descriptionText != null)
        {
            descriptionText.text = option.Title + "\n" + option.Description;
        }
    }
    public void OnClick()
    {
        UpgradeService.ApplyTo(GameManager.Instance.Player.gameObject, upgradeOption);
        InteractionEvents.RaiseCloseUpgradePanel();
    }
}
