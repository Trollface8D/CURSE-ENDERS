﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public GameObject EnemyObj;

    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int damage)
    {
        GetComponent<EnemyAI>().knockback(0.2f,10f);
        currentHealth -= damage;
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
