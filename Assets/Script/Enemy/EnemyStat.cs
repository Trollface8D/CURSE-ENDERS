using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public HealthBarBehavior Healthbar;

    private GameObject Player;

    public GameObject EnemyObj;

    public bool staggered = false;

    public int dropKobold = 0; //max 5
    public int dropSnail = 0;//max 5
    public int dropPlantae = 0;//max 5
    public int dropGrim = 0;//max 2


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
        Object.Destroy(EnemyObj);
        GameObject.Find("EnemySpawnerScript").GetComponent<EnemySpawner>().EnemyReduct(1);
        if(Player.GetComponent<PlayerStat>().KoboldGate < 3)
        {
            Player.GetComponent<PlayerStat>().KoboldGate+=dropKobold;
        }
        if (Player.GetComponent<PlayerStat>().SnailGate < 5)
        {
            Player.GetComponent<PlayerStat>().SnailGate+=dropSnail;
        }
        if (Player.GetComponent<PlayerStat>().PlantaeGate < 5)
        {
            Player.GetComponent<PlayerStat>().PlantaeGate += dropPlantae;
        }
        if (Player.GetComponent<PlayerStat>().GrimGate < 2)
        {
            Player.GetComponent<PlayerStat>().GrimGate+=dropGrim;
        }
        Scorescript.scoreValue += DiedScore;
    }
}
