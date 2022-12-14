using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool doublejump = true;
    private bool jumping = false;

    [HideInInspector]
    public bool die = false;
    public Animator animator;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform RightWallCheck;
    [SerializeField] private LayerMask groundLayer;


    private void Start()
    {
        die = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ignore colision for enemy
        Physics2D.IgnoreLayerCollision(9,10);

        horizontal = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontal * speed));
        if (Mathf.Abs(rb.velocity.y) > 0)
        {
            animator.SetBool("jumping", true);
        }
        else
        {
            animator.SetBool("jumping", false);
        }
        if (IsGrounded() && jumping)
        {
            jumping = false;
            doublejump = true;

        }

        if (Input.GetButtonDown("Jump"))
        {
            jumping = true;
        }

        if (Input.GetButtonDown("Jump") && (IsGrounded()))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }


        //mutiple jump
        else if(Input.GetButtonDown("Jump") && (doublejump) && !IsGrounded()) //double jump : remove "Iswall()"
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            doublejump = false;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        filp();
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer);
    }
    //private bool IsWall()
    //{
    //    if (Physics2D.OverlapCircle(LeftWallCheck.position, 0.2f, groundLayer) || Physics2D.OverlapCircle(RightWallCheck.position, 0.2f, groundLayer))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}

    private void FixedUpdate()
    {
        if (die)
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        else if(!die)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private void filp()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;

            transform.Rotate(0f, 180f, 0f);
        }
    }
}
