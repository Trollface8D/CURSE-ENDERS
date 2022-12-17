using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public HealthBarBehavior Healthbar;

    public GameObject EnemyObj;

    public bool staggered = false;

    public int maxHealth = 100;
    public int currentHealth;

    public int DiedScore=10;

    void Start()
    {
        currentHealth = maxHealth;
        Healthbar.SetHealth(currentHealth, maxHealth);
    }

    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Healthbar.SetHealth(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        GameObject.Find("EnemySpawnerScript").GetComponent<EnemySpawner>().EnemyReduct(1);
        Object.Destroy(EnemyObj);
        Scorescript.scoreValue -= DiedScore;
    }
}
