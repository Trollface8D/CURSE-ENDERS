using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackpoint;
    public LayerMask enemyLayers;
    public Animator animator;

    public int attackDamage = 40;
    public float attackRange = 5f;

    public float attackRate = 3f;
    private float nextAttackTime = 0f;

    private bool attack;


    // Update is called once per frame
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        else if(Time.time <= nextAttackTime){
            animator.SetBool("attack",false);
        }
        
    }
    void Attack()
    {
        animator.SetBool("attack",true);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStat>().TakeDamage(attackDamage);
            //Debug.Log("Hit :" + enemy.GetComponent<EnemyStat>().name + " " + enemy.GetComponent<EnemyStat>().currentHealth);
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
