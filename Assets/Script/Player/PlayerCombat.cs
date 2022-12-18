using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackpoint;
    public Transform firePoint;

    public PlayerStat playerStat;

    public LayerMask enemyLayers;
    public Animator animator;

    private bool HardBulletState = false;
    public float HardBulletTime = 5f;
    private float nextHardBulletTime = 0f;

    public int attackDamage = 40;
    public float attackRange = 5f;

    public float attackRate = 3f;
    private float nextAttackTime = 0f;

    public float fireRate = 2f;
    private float nextFireTime = 0f;

    public int HardHitDamage = 90;
    public float HardhitRate = 10f;
    private float nextHardhitTime = 0f;

    public GameObject bulletPrefab;
    public GameObject HardbulletPrefab;

    [SerializeField] private AudioSource attacksoundeffect;
    [SerializeField] private AudioSource throwsoundeffect;
     

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextHardhitTime && GetComponent<PlayerStat>().KoboldGate >=5) //Heavy Attack
        {
            if (Input.GetButtonDown("1"))
            {
                HardHit();
                GetComponent<PlayerStat>().KoboldGate = 0;
                nextHardhitTime = Time.time + HardhitRate;
            }
        }
        else if (Time.time <= nextHardhitTime)
        {
            animator.SetBool("heavyattack", false);
            animator.SetBool("attack", false);
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        else if(Time.time <= nextAttackTime)
        {
            animator.SetBool("attack",false);
        }
        //Shooting
        if (Input.GetButtonDown("2") && GetComponent<PlayerStat>().SnailGate >= 5)
        {
            HardBulletState = true;
            nextHardBulletTime = Time.time + HardBulletTime;
            GetComponent<PlayerStat>().SnailGate = 0;
        }
        else if (Time.time >= nextHardBulletTime && nextHardBulletTime != 0f)
        {
            HardBulletState = false;
            nextHardBulletTime = 0f;
        }

        if (!HardBulletState)
        {
            if (Time.time >= nextFireTime)
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    Shoot();
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
            else
            {
                animator.SetBool("Shoot", false);
            }
        }
        else if (HardBulletState)
        {
            if (Time.time >= nextFireTime)
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    HardShoot();
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
            else
            {
                animator.SetBool("HardShoot", false);
            }
        }

        if(Input.GetButtonDown("3") && GetComponent<PlayerStat>().PlantaeGate >= 5)
        {
            if(GetComponent<PlayerStat>().currentHealth+50 > 100)
            {
                GetComponent<PlayerStat>().currentHealth = 100;
            }
            else if (GetComponent<PlayerStat>().currentHealth + 50 <= 100)
            {
                GetComponent<PlayerStat>().currentHealth += 50;
            }
            GetComponent<PlayerStat>().PlantaeGate = 0;
        }
  
    }


    void Shoot()
    {
        animator.SetBool("Shoot", true);
        throwsoundeffect.Play();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void HardShoot()
    {
        animator.SetBool("HardShoot", true);
        throwsoundeffect.Play();
        Instantiate(HardbulletPrefab, firePoint.position, firePoint.rotation);
    }
    void Attack()
    {
        animator.SetBool("attack",true);
        attacksoundeffect.Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStat>().TakeDamage(attackDamage);
            enemy.GetComponent<EnemyAI>().Knockback(0.1f, 20f);
            enemy.GetComponent<EnemyAI>().Freeze(0.3f);
        }
    }

    void HardHit()
    {
        animator.SetBool("heavyattack", true);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStat>().TakeDamage(HardHitDamage);
            enemy.GetComponent<EnemyAI>().Knockback(0.1f, 40f);
            enemy.GetComponent<EnemyAI>().Freeze(5f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackpoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }
}
