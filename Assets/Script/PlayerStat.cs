using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour
{

    public int maxHealth1 = 100;
    public static int ultGate = 0;
    int currentHealth;

    public PlayerHealthBar playHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth1;
        playHealth.SetMaxHealth1(maxHealth1);
    }

    // Update is called once per frame
    public void TakeDamage1(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        playHealth.SetHealth1(currentHealth);
    }
    void Die()
    {
        Application.LoadLevel(Application.loadedLevel);
        Debug.Log("Restarted");
    }
}
