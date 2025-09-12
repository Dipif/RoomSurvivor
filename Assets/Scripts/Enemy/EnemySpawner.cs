using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Enemy enemyPrefab;
    public List<Enemy> enemies = new List<Enemy>();
    public float spawnInterval = 1f;

    private float spawnTimer;

    bool isSpawning = false;

    private void OnEnable()
    {
        RoomEvents.OnRoomEntered += OnRoomEntered;
        RoomEvents.OnRoomExited += OnRoomExited;
    }

    private void OnDisable()
    {
        RoomEvents.OnRoomEntered -= OnRoomEntered;
        RoomEvents.OnRoomExited -= OnRoomExited;
    }

    void OnRoomEntered(RoomBase room)
    {
        spawnTimer = spawnInterval;
        if (IsSpawningInRoom(room))
            isSpawning = true;
    }

    void OnRoomExited(RoomBase room)
    {
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
            enemy.transform.position = Vector3.zero;
        }
        spawnTimer = 0f;
        isSpawning = false;
    }

    private void Update()
    {
        if (!isSpawning)
            return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemyInRoom();
        }
    }

    public void Restart()
    {
        foreach (var enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
            enemy.transform.position = Vector3.zero;
        }
        isSpawning = false;
    }

    private bool IsSpawningInRoom(RoomBase room)
    {
        Debug.Log(room.GetType().Name);
        if (room as SkillRoom != null)
            return false;
        else if (room as LevelRoom != null)
            return false;
        return true;
    }

    private void SpawnEnemyInRoom()
    {
        RoomBase currentRoom = GameManager.Instance.RoomManager.CurrentRoom;

        Collider roomCollider = currentRoom.GetComponent<Collider>();
        Vector3 spawnPos = GetRandomPositionInCollider(roomCollider);

        Vector2 playerPos2D = new Vector2(GameManager.Instance.Player.transform.position.x, GameManager.Instance.Player.transform.position.z);
        Vector2 spawnPos2D = new Vector2(spawnPos.x, spawnPos.z);
        while (Vector2.Distance(spawnPos2D, playerPos2D) < 5.0f)
        {
            spawnPos = GetRandomPositionInCollider(roomCollider);
            spawnPos2D = new Vector2(spawnPos.x, spawnPos.z);
        }

        Enemy enemy = GetInactiveEnemy();
        if (enemy == null)
        {
            enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemies.Add(enemy);
        }
        else
        {
            enemy.transform.position = spawnPos;
            enemy.gameObject.SetActive(true);
        }
        enemy.Init();
    }

    private Enemy GetInactiveEnemy()
    {
        foreach (var enemy in enemies)
        {
            if (!enemy.gameObject.activeSelf)
                return enemy;
        }
        return null;
    }

    private Vector3 GetRandomPositionInCollider(Collider col)
    {
        Bounds bounds = col.bounds;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;
        Vector3 pos = new Vector3(
            Random.Range(min.x, max.x),
            1.0f,
            Random.Range(min.z, max.z)
        );
        return pos;
    }
}
