using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();

    public Transform[] spawnLocation;
    private int spawnIndex;

    public int EnemySpawned = 0;

    private int Wave = 1;
    public int WaveValue = 10;
    private int currentWaveValue;

    private float spawnCooldown = 2f;
    private float currentspawnCooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
        currentWaveValue = WaveValue;
    }
    private void FixedUpdate()
    {
        currentspawnCooldown += Time.deltaTime;
        if (currentWaveValue <= 0 && EnemySpawned<=0)
        {
            currentspawnCooldown = 0f;
            currentspawnCooldown = -3f;
            Wave += 1;
            currentWaveValue = 10*Wave;
        }
        if (WaveValue > 0)
        {
            spawnIndex = Random.Range(0, spawnLocation.Length);
            
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (currentWaveValue - randEnemyCost >= 0 && currentspawnCooldown>=spawnCooldown)
            {
                spawnEnemy(enemies[randEnemyId].enemyPrefab);
                currentWaveValue -= randEnemyCost;
                currentspawnCooldown = 0f;
            }
            if(currentWaveValue - randEnemyCost < 0 && currentWaveValue>0 && currentspawnCooldown >= spawnCooldown)
            {
                currentWaveValue -= 1;
            }
        }

    }

    private void spawnEnemy(GameObject enemy)
    {
            GameObject newEnemy = Instantiate(enemy, spawnLocation[spawnIndex].position, Quaternion.identity);
            EnemySpawned++;
    }
    public void EnemyReduct(int value)
    {
        EnemySpawned -= value;
    }
}

[System.Serializable]
public class Enemy
{
    public GameObject enemyPrefab;
    public int cost;
}
