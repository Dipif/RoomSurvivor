using UnityEngine;
public class SceneMove : MonoBehaviour
{
    public string sceneName;
    public void MoveToScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
