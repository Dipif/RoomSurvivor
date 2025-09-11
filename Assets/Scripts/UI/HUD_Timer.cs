using TMPro;
using UnityEngine;

public class HUD_Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float timer = 0f;

    private void OnEnable()
    {
        GameManager.Instance.RestartChannel.OnRaised += ResetTimer;
    }

    private void OnDisable()
    {
        GameManager.Instance.RestartChannel.OnRaised -= ResetTimer;
    }

    private void Start()
    {
        ResetTimer();
    }
    void ResetTimer()
    {
        timer = 0f;
        UpdateTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
