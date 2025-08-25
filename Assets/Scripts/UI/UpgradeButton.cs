using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : Button
{
    public void Init(string title, string description, System.Action onClick)
    {
        var text = GetComponentInChildren<TextMeshProUGUI>();
        if (text != null)
        {
            text.text = title + "\n" + description;
        }
        GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        GetComponentInChildren<Button>().onClick.AddListener(() => onClick());
    }
    public void OnClick()
    {
        // This method will be linked to the button's OnClick event in the Unity Editor.
        Debug.Log("Upgrade button clicked!");
        InteractionEvents.RaiseCloseUpgradePanel();
    }
}
