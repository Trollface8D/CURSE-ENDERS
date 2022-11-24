using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackpoint;
    public LayerMask playerLayer;

    public int attackDamage = 10;
    public float attackRange = 0.2f;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Physics2D.OverlapCircle(attackpoint.position, attackRange, playerLayer))
            {
                Debug.Log("Enemy Attack Sucesful");
                GetComponent<EnemyAI>().knockback(0.6f, 7f);
                AttackPlayer();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

    }
    void AttackPlayer() 
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerStat>().TakeDamage1(attackDamage);
            //Debug.Log("Hit :" + enemy.GetComponent<EnemyStat>().name + " " + enemy.GetComponent<EnemyStat>().currentHealth);
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackpoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position, attackRange);
    }
}
