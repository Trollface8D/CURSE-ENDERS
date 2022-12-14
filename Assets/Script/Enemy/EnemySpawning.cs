using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public GameObject[] spawnPoints;
    GameObject currentPoint;
    int index;

    public GameObject[] enemies;
    public float minTimeBtwSpawns;
    public float maxTimeBtwSpawns;
    public bool canspawn;
    public float spawnTime;
    public int enemiesInRoom;
    public bool spawnerDone;
    public GameObject spawnerDoneGameObject;

    void Start()
    {
        Invoke("SpawnEnemy", 0.5f);
    }

    private void Update()
    {
        if(canspawn)
        {
            spawnTime -= Time.deltaTime;
            if(spawnTime < 0)
            {
                canspawn = false;
            }
        }
    }

    void SpawnEnemy()
    {
        index = Random.Range(0, spawnPoints.Length);
        currentPoint = spawnPoints[index];
        float TimeBtwSpawns = Random.Range(minTimeBtwSpawns, maxTimeBtwSpawns);

        if(canspawn){
            Instantiate(enemies[Random.Range(0, enemies.Length)], currentPoint.Transform.position, Quaternion.identity);
            //rondomly choose to spawn enemies in array
            enemiesInRoom++;
        }

        Invoke("SpawnEnemy", TimeBtwSpawns);
        if(spawnerDone){
            // Done spawn
            spawnerDoneGameObject.SetActive(true);
        }
    }
}
