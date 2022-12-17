using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackpoint;
    public LayerMask playerLayer;

    public int attackDamage = 10;
    public float attackRange = 0.2f;

    public float attackRate = 1f;
    private float nextAttackTime = 0f;

    [SerializeField] private AudioSource attacksoundeff;

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(attackpoint.position, attackRange, playerLayer) && !GetComponent<EnemyStat>().staggered)
        {
            Debug.Log("Enemy Charging");
            GetComponent<EnemyAI>().FreeFreeze(attackRate);
            nextAttackTime += Time.deltaTime;
            if (nextAttackTime >= attackRate && !GetComponent<EnemyStat>().staggered)
            {
                Debug.Log("Enemy Attack Sucesful");
                //GetComponent<EnemyAI>().knockback(0.6f, 7f);//temporary after hit action
                AttackPlayer();
                nextAttackTime = 0f;
                //nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        else
        {
            nextAttackTime = 0f;
        }
    }
    void AttackPlayer() 
    {
        attacksoundeff.Play();
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackpoint.position, attackRange, playerLayer);
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerStat>().TakeDamage1(attackDamage);
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
