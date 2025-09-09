using UnityEngine;

public class ResultManager : MonoBehaviour
{
    [SerializeField] HUD_Result HUD_Result;
    public void ProcessGameOver()
    {
        HUD_Result.Show();
    }
}
