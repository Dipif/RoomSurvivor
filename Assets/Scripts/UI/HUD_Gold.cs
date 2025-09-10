using TMPro;
using UnityEngine;

public class HUD_Gold : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldText;
    // Update is called once per frame
    void Update()
    {
        goldText.text = GameManager.Instance.Gold.ToString();
    }
}
