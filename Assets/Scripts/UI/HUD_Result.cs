using TMPro;
using UnityEngine;

public class HUD_Result : MonoBehaviour
{
    [SerializeField] GameObject resultUI;
    [SerializeField] TextMeshProUGUI resultText;
    public void Show()
    {
        resultText.text = $@"게임 오버
점수: {GameManager.Instance.Score}";
        resultUI.SetActive(true);
    }

    public void Hide()
    {
        resultUI.SetActive(false);
    }

    public void OnRestartButton()
    {
        Hide();
        GameManager.Instance.Restart();
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
