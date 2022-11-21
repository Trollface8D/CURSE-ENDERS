﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float startspeed = 20f;
    public float chasespeed = 40f;
    private float walkSpeed = 20f;
    [HideInInspector]

    public bool mustpatrol;

    public Rigidbody2D rb;

    public Transform groundCheckPos;
    public Transform WallCheck;
    public LayerMask groundlayer;
    public LayerMask Playerlayer;

    public Transform Player;
    public Transform Enemy;

    public float filprange = 0.3f;

    public float nextFreezetime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        mustpatrol = true;
        walkSpeed = startspeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustpatrol)
        {
            rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime * 10, rb.velocity.y);
        }

        //AI follow player
        if (Mathf.Abs(Player.position.x - Enemy.position.x) <filprange)
        {
            
        }
        else if (!IsPlayerinrange())
        {
            //AI Patrol player
            if (IsWall() || IsFilp())
            {
                Flip();
            }
            //
            if (walkSpeed >= 0f)
            {
                walkSpeed = startspeed;
            }
            else
            {
                walkSpeed = -startspeed;
            }
        }
        else if (IsPlayerinrange())
        {
            //Debug.Log("Hitting");

            if (walkSpeed > 0f)
            {
                walkSpeed = chasespeed;
                if ((Player.position.x - Enemy.position.x) < 0)
                {
                    Flip();
                }
                
            }
            else if(walkSpeed < 0f)
            {
                walkSpeed = -chasespeed;
                if ((Player.position.x - Enemy.position.x) > 0)
                {
                    Flip();
                }
            }
            //if (IsWall() || IsFilp())
            //{
            //    Flip();
            //}
        }
              
    }
    //end follow player
    void Flip()
    {
        mustpatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        //direction *= -1;
        walkSpeed *= -1;
        mustpatrol = true;
    }
    private bool IsWall()
    {
        return Physics2D.OverlapCircle(WallCheck.position, 0.2f, groundlayer);
    }

    private bool IsFilp()
    {
        return !Physics2D.OverlapCircle(groundCheckPos.position, 0.2f, groundlayer);
    }
    private bool IsPlayerinrange()
    {
        return Physics2D.OverlapBox(new Vector2(Enemy.position.x, Enemy.position.y + 0.625f), new Vector2(20f,2f),0, Playerlayer);
        //return Physics2D.OverlapCircle(Enemy.position, 3f, Playerlayer);
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
    public void Freeze(float duration)
    {
        float temp = walkSpeed;
        float current = Time.time;
        walkSpeed = 0;
        if (Time.time >= current + duration)
        {
            walkSpeed = temp;
        }
    }
}
