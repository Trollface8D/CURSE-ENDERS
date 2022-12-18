using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 2;
    public float knockPower = 5f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitinfo)
    {
        EnemyStat enemy = hitinfo.GetComponent<EnemyStat>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
            enemy.GetComponent<EnemyAI>().Knockback(0.3f, knockPower);
        }
        if (!(hitinfo.gameObject.layer == 9))// 9 is Player layer
        {
            Destroy(gameObject);
        }
    }
}
