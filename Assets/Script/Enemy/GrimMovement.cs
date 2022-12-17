using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimMovement : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform Player;
    [SerializeField]
    private float teleportTimer=2f;
    private float nextTeleport = 0f;
    private bool readytoshoot = false;

    [SerializeField] private AudioSource lasersoundeff;

    private void Start()
    {
        Player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        nextTeleport += Time.deltaTime;
        if (nextTeleport >= teleportTimer)
        {
            Teleport(Random.Range(-8.5f, 8.5f),-1.3f);

            if (!readytoshoot)
            {
                readytoshoot = true;
            }
            nextTeleport = 0f;
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
