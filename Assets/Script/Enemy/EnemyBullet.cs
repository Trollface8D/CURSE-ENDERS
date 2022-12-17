using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 5;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitinfo)
    {
        PlayerStat player = hitinfo.GetComponent<PlayerStat>();
        if (player != null)
        {
            player.TakeDamage1(damage);
        }
        if(!(hitinfo.gameObject.layer == 10))// 10 is Enemy layer
        {
            Destroy(gameObject);
        }
    }
}
