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
    public SoulBar soulBar;
    public SoulBar1 soulBar1;
    public SoulBar2 soulBar2;
    public SoulBar3 soulBar3;

    private float Deadtime = 3f;


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
        soulBar.SetMax(3);
        soulBar1.SetMax(5);
        soulBar2.SetMax(5);
        soulBar3.SetMax(2);
    }
    private void Update()
    {
        playHealth.SetHealth1(currentHealth);
        soulBar.Setcurrent(KoboldGate);
        soulBar1.Setcurrent(SnailGate);
        soulBar2.Setcurrent(PlantaeGate);
        soulBar3.Setcurrent(GrimGate);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Deadtime -= Time.deltaTime;
            if(Deadtime <= 2.8f)
            {
                GetComponent<PlayerMovement>().enabled = false;
            }
            if (Deadtime <= 0)
            {
                Deadtime = 3f;
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    // Update is called once per frame
    public void TakeDamage1(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Deadtime -= Time.deltaTime;
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
        GetComponent<PlayerMovement>().die = true;
        GetComponent<PlayerCombat>().enabled = false;
        animator.SetBool("playerded",true);
        //Put these in change scene pls
        EnemySpawner.Wave = 1;
        Scorescript.scoreValue = 0;
    }
    
}
