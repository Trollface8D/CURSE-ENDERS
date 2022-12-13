using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public HealthBarBehavior Healthbar;

    public GameObject EnemyObj;

    public bool staggered = false;

    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Healthbar.SetHealth(currentHealth, maxHealth);
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Healthbar.SetHealth(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Object.Destroy(EnemyObj);
    }
}
