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

    void Start()
    {
        
    }

    void Update()
    {
        if(enemiesInScene < maxEnemiesInScene)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {

        enemiesInScene++;
    }

    public void ChooseNewPosition()
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
    }
}
