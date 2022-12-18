using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStat : MonoBehaviour
{
    public int maxHealth1 = 100;
    public static int ultGate = 0;
    public int currentHealth;

    public PlayerHealthBar playHealth;

    public Animator animator;
    private bool playerded;

    public int KoboldGate = 0;
    public int SnailGate = 0;
    public int PlantaeGate = 0;
    public int GrimGate = 0;

    [SerializeField] private AudioSource playerdedsoundeffect;

    void Start()
    {
        currentHealth = maxHealth1;
        playHealth.SetMaxHealth1(maxHealth1);
    }
    private void Update()
    {
        playHealth.SetHealth1(currentHealth);
    }

    // Update is called once per frame
    public void TakeDamage1(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    
    void Die()
    {
        // Application.LoadLevel(Application.loadedLevel);
        if (!(animator.GetBool("playerded")))
        {
            playerdedsoundeffect.Play();
        }
        GetComponent<PlayerCombat>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
        animator.SetBool("playerded",true);
        SceneManager.LoadScene("GameOver");
    }
    
}
