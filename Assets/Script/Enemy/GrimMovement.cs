using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimMovement : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform Player;
    [SerializeField]
    private float appearTimer = 4f;
    [SerializeField]
    private float invisbleTimer = 2f;

    private float nextAppear = 0f;
    private float nextInvisble = 0f;
    private bool readytoshoot = false;

    public Animator animator;
    [SerializeField] private AudioSource lasersoundeff;

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        nextInvisble += Time.deltaTime;
        if (nextInvisble >= invisbleTimer)
        {
            if (animator.GetBool("invisble"))
            {
                Teleport(Random.Range(-8.5f, 8.5f), -1.3f);
            }
            animator.SetBool("invisble", false);
            GetComponent<Collider2D>().enabled = true;
            nextAppear += Time.deltaTime;
            if (nextAppear >= appearTimer)
            {
                if (!readytoshoot)
                {
                    readytoshoot = true;
                }
                animator.SetBool("invisble", true);
                nextInvisble = 0f;
                nextAppear = 0f;
                GetComponent<Collider2D>().enabled = false;
            }
        }
        if (Player.GetComponent<PlayerMovement>().IsGrounded()&&readytoshoot)
        {
            readytoshoot = false;
            LaserShoot();
        }
    }
    public void Teleport(float Xvalue, float Yvalue)
    {
        transform.position = new Vector2(Xvalue, Yvalue);
    }
    void LaserShoot()
    {
        lasersoundeff.Play();
        Instantiate(laserPrefab, new Vector2(Player.position.x, Player.position.y-0.5f), Player.rotation);
    }
}
