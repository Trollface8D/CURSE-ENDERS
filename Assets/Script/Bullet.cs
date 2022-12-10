using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 2;
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
            if (enemy.GetComponent<EnemyStat>().staggered)
            {
                enemy.TakeDamage(damage*10);
                enemy.GetComponent<EnemyAI>().knockback(0.1f, 20f);
            }
            else
            {
                enemy.TakeDamage(damage);
                enemy.GetComponent<EnemyAI>().knockback(0.3f, 4f);
            }
            
        }
        Destroy(gameObject);
    }
}
