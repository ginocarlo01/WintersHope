using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    List<Transform> spawnPoints;

    [SerializeField]
    List<Transform> occupiedPoints;

    [SerializeField]
    int maxEnemiesInScene = 1;

    int enemiesInScene = 0;

    [SerializeField]
    GameObject enemyPreFab;

    public static SpawnEnemies instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        CheckToSpawnEnemy();
    }

    public void CheckToSpawnEnemy()
    {
        if (enemiesInScene < maxEnemiesInScene)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        Transform spawnPos = ChooseNewPosition();
        OccupyPoint(spawnPos);
        Instantiate(enemyPreFab, spawnPos.position, Quaternion.identity);
        enemiesInScene++;
    }

    public Transform ChooseNewPosition()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
        Transform randomSpawnPoint = spawnPoints[randomIndex];
        return randomSpawnPoint;
    }

    public void OccupyPoint(Transform point)
    {
        spawnPoints.Remove(point);
        occupiedPoints.Add(point);
    }

    public void FreePoint(Transform point)
    {
        occupiedPoints.Remove(point);
        spawnPoints.Add(point);
    }

    public Transform ChangeObjectPosition(Transform oldPosition)
    {
        Transform newPosition = ChooseNewPosition();
        FreePoint(oldPosition);
        OccupyPoint(newPosition);
        return newPosition;
    }
}
