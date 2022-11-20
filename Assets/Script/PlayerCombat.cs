using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackpoint;
    public LayerMask enemyLayers;

    public int attackDamage = 40;
    public float attackRange = 0.5f;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;


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
        
    }
    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit :" + enemy.name);
            enemy.GetComponent<EnemyStat>().TakeDamage(attackDamage);
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
