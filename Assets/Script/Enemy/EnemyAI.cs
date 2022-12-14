using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float startspeed = 15f;
    public float chasespeed = 25f;
    private float walkSpeed = 20f;


    //public float strength = 10f;
    public float knockDuration = 0.2f;
    float hitTimeKnockback;
    float hitTimeFreeze;

    private bool mustpatrol;
    private bool OnKnock;
    private bool OnFreeze;
    [HideInInspector]
    public bool onChase = false;

    public Rigidbody2D rb;

    public Transform groundCheckPos;
    public Transform WallCheck;
    public LayerMask groundlayer;
    public LayerMask Playerlayer;

    public Transform Player;
    public Transform Enemy;

    public float filprange = 0.3f;

    float FreezeDuration = 0f;

    // Start is called before the first frame update
    void Start()
    {
        mustpatrol = true;
        walkSpeed = startspeed;
    }

    // Update is called once per frame
    void Update()
    {
        // enemy layer collision ignore
        Physics2D.IgnoreLayerCollision(10, 10);
        if (OnFreeze && !OnKnock)
        {
            rb.velocity = new Vector2(0 * Time.fixedDeltaTime * 10, rb.velocity.y);
        }

        else if (mustpatrol)
        {
            rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime * 10, rb.velocity.y);
        }

        //AI follow player
        if (!OnFreeze)
        {
            if (Mathf.Abs(Player.position.x - Enemy.position.x) < filprange)
            {

            }
            else if (!IsPlayerinrange())
            {
                onChase = false;
                //AI Patrol player
                if (IsWall() || IsGrounded())
                {
                    Flip();
                }
            }
            else if (IsPlayerinrange())
            {
                onChase = true;
                if (walkSpeed > 0f)
                {
                    walkSpeed = chasespeed;
                    if ((Player.position.x - Enemy.position.x) < 0)
                    {
                        Flip();
                    }

                }
                else if (walkSpeed < 0f)
                {
                    walkSpeed = -chasespeed;
                    if ((Player.position.x - Enemy.position.x) > 0)
                    {
                        Flip();
                    }
                }
            }
        }

        if (OnKnock && Time.time >= hitTimeKnockback + knockDuration)
        {
            mustpatrol = true;
            OnKnock = false;
        }
        if (OnFreeze)
        {
            if (Time.time >= hitTimeFreeze + FreezeDuration)
            {
                FreezeDuration = 0f;
                mustpatrol = true;
                OnFreeze = false;
                GetComponent<EnemyStat>().staggered = false;
            }
        }
    }
    //end follow player
    void Flip()
    {
        mustpatrol = false;
        transform.Rotate(0f, 180f, 0f);
        //transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustpatrol = true;
    }
    private bool IsWall()
    {
        return Physics2D.OverlapCircle(WallCheck.position, 0.2f, groundlayer);
    }

    private bool IsGrounded()
    {
        return !Physics2D.OverlapCircle(groundCheckPos.position, 0.2f, groundlayer);
    }
    private bool IsPlayerinrange()
    {
        return Physics2D.OverlapBox(new Vector2(Enemy.position.x, Enemy.position.y + 0.625f), new Vector2(20f, 2f), 0, Playerlayer);
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 enemypos;
        enemypos = new Vector2(Enemy.position.x, Enemy.position.y + 0.625f);
        if (enemypos == null)
        {
            return;
        }
        Gizmos.DrawWireCube(enemypos, new Vector2(20f, 2f));
    }

    public void knockback(float KnockDuration, float strength)
    {
        knockDuration = KnockDuration;
        hitTimeKnockback = Time.time;
        OnKnock = true;
        mustpatrol = false;
        Vector2 direction = (transform.position - Player.transform.position).normalized;
        rb.AddForce(direction * strength, ForceMode2D.Impulse);
    }

    public void Freeze(float duration)
    {
        hitTimeFreeze = Time.time;
        FreezeDuration = duration;
        OnFreeze = true;
        mustpatrol = false;
        GetComponent<EnemyStat>().staggered = true;
    }
    public void FreeFreeze(float duration)
    {
        hitTimeFreeze = Time.time;
        FreezeDuration = duration;
        OnFreeze = true;
        mustpatrol = false;
    }
}