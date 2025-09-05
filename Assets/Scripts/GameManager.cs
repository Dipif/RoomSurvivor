using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Character Player;
    public RoomManager RoomManager;

    public int Gold = 0;
    public int Score = 0;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    void Init()
    {
        Gold = 0;
        Score = 0;
    }

    public RoomBase GetCurrentRoom()
    {
        return RoomManager.CurrentRoom;
    }
}
