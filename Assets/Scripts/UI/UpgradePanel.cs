using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField]
    private GameObject root;

    [SerializeField]
    private UpgradeButton buttonPrefab;

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
            var button = buttonObj.GetComponent<Button>();
            var text = buttonObj.GetComponentInChildren<Text>();
            text.text = option.Title + "\n" + option.Description;
            int idx = i; // Capture index for the lambda
            button.onClick.AddListener(() => Choose(idx));
        }
        root.SetActive(true);
    }

    private void Close()
    {
        root.SetActive(false);
    }

    private void Choose(int idx)
    {

    }

}
