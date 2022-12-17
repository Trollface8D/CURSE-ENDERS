using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public HealthBarBehavior Healthbar;

    private GameObject Player;

    public GameObject EnemyObj;

    public bool staggered = false;

    public int dropKobold = 0;
    public int dropSnail = 0;
    public int dropGrim = 0;


    public int maxHealth = 100;
    public int currentHealth;

    public int DiedScore=10;


    void Start()
    {
        Player = GameObject.Find("Player");
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
        Player.GetComponent<PlayerStat>().KoboldGate+=dropKobold;
        Player.GetComponent<PlayerStat>().SnailGate+=dropSnail;
        Player.GetComponent<PlayerStat>().GrimGate+=dropGrim;
        Object.Destroy(EnemyObj);
        Scorescript.scoreValue -= DiedScore;
    }
}
