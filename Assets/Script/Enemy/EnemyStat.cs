using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public GameObject EnemyObj;

    public bool staggered = false;

    public int maxHealth = 100;
    public int currentHealth;

     private EnemySpawning enemySpawning;

    void Start()
    {
        currentHealth = maxHealth;
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Object.Destroy(EnemyObj);
        Scorescript.scoreValue += 10;
        enemySpawning = FindObjectOftype<EnemySpawning>(Koblod);
        enemySpawning.enemiesInRoom--;
        if(enemySpawning.spawnTime <= 0 && enemySpawning.enemiesInRoom <= 0){
            //make sure that all enemy are dead in the room
            enemySpawning.spawnerDone = true;
        }
    }
}
