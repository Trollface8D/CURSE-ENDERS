using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform attackpoint;
    public Transform triggerAttackpoint;
    public LayerMask playerLayer;

    public int attackDamage = 10;
    public float attackRange = 0.2f;

    public float attackRate = 1f;
    private float nextAttackTime = 0f;

    public Animator animator;

    [SerializeField] private AudioSource attacksoundeff;

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(triggerAttackpoint.position, attackRange, playerLayer) && !GetComponent<EnemyStat>().staggered)
        {
            animator.SetBool("Attack", true);
            Debug.Log("Enemy Charging");
            GetComponent<EnemyAI>().FreeFreeze(attackRate);
            nextAttackTime += Time.deltaTime;
            if (nextAttackTime >= attackRate && !GetComponent<EnemyStat>().staggered)
            {
                Debug.Log("Enemy Attack Sucesful");
                AttackPlayer();
                nextAttackTime = 0f;
            }
        }
        else
        {
            animator.SetBool("Attack", false);
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
