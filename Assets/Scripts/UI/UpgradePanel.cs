using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject root;

    [SerializeField]
    private HUD_UpgradeItem buttonPrefab;

    private List<UpgradeOption> options;
    private PlayerInteractor interactor;

    private void OnEnable()
    {
        InteractionEvents.OnOpenUpgradePanel += Open;
        InteractionEvents.OnCloseUpgradePanel += Close;
    }

    private void OnDisable()
    {
        InteractionEvents.OnOpenUpgradePanel -= Open;
        InteractionEvents.OnCloseUpgradePanel -= Close;
    }

    public void OnClick()
    {
        Close();
    }

    private void Open(List<UpgradeOption> options, PlayerInteractor interactor)
    {
        this.options = options;
        this.interactor = interactor;
        foreach (Transform child in root.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < options.Count; i++)
        {
            var option = options[i];
            var buttonObj = Instantiate(buttonPrefab, root.transform);
            HUD_UpgradeItem upgradeButton = buttonObj.GetComponent<HUD_UpgradeItem>();
            upgradeButton.Init(option, ()=> Choose(i));
        }
        root.SetActive(true);
    }

    private void Close()
    {
        root.SetActive(false);
    }

    private void Choose(int idx)
    {
        Debug.Log($"Chosen upgrade option {idx}");
    }

}
