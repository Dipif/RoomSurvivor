using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Character Player;
    public EnemySpawn EnemySpawn;
    public RoomManager RoomManager;
    public ResultManager ResultManager;
    public FloatingTextPool DamageTextPool;
    public FloatingTextPool GoldTextPool;

    public FloatingTextTheme FloatingTextTheme;

    public int Gold = 0;
    public int Score = 0;

    public GameRestartEventChannel RestartChannel;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Init();
    }

    private void OnEnable()
    {
        if (Player != null)
        { 
            CharacterStatus playerStatus = Player.GetComponent<CharacterStatus>();
            playerStatus.OnDied += HandlePlayerDied;
        }
    }

    private void OnDisable()
    {
        if (Player != null)
        {
            CharacterStatus playerStatus = Player.GetComponent<CharacterStatus>();
            playerStatus.OnDied -= HandlePlayerDied;
        }
    }

    private void HandlePlayerDied()
    {
        Time.timeScale = 0f;

        ResultManager?.ProcessGameOver();
    }

    public void Restart()
    {
        Init();
        RoomManager?.Restart();
        Player?.Restart();
        EnemySpawn?.Restart();
        RestartChannel.Raise();
    }

    void Init()
    {
        Time.timeScale = 1f;
        Gold = 0;
        Score = 0;
    }

    public RoomBase GetCurrentRoom()
    {
        return RoomManager.CurrentRoom;
    }
}
