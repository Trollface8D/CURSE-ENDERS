﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackpoint;
    public Transform firePoint;

    public LayerMask enemyLayers;
    public Animator animator;

    public int attackDamage = 40;
    public float attackRange = 5f;

    public float attackRate = 3f;
    private float nextAttackTime = 0f;

    public float fireRate = 2f;
    private float nextFireTime = 0f;

    public float HardhitRate = 10f;
    private float nextHardhitTime = 0f;


    public GameObject bulletPrefab;

    [SerializeField] private AudioSource attacksoundeffect;
    [SerializeField] private AudioSource throwsoundeffect;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextHardhitTime)
        {
            if (Input.GetButtonDown("LeftShift"))
            {
                HardHit();
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
        if(Time.time >= nextFireTime)
        {
            if (Input.GetButtonDown("Fire2"))
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }


    void Shoot()
    {
        throwsoundeffect.Play();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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
            enemy.GetComponent<EnemyAI>().Knockback(0.1f, 40f);
            enemy.GetComponent<EnemyAI>().Freeze(5f);
        }
    }

    void Ultimate()
    {

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
