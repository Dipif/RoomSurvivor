using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD_UpgradeItem : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI descriptionText;

    public void Init(string title, string description, System.Action onClick)
    {
        if (descriptionText != null)
        {
            descriptionText.text = title + "\n" + description;
        }
    }
    public void OnClick()
    {
        // This method will be linked to the button's OnClick event in the Unity Editor.
        Debug.Log("Upgrade button clicked!");
        InteractionEvents.RaiseCloseUpgradePanel();
    }
}
