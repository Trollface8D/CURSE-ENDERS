using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimLaser : MonoBehaviour
{
    public int damage =20;
    public float shootTime = 2f;
    public float chargeTime = 1f;
    public float existTime = 2.3f;
    private float nextShootTime = 0f;
    // Start is called before the first frame update
    void Update()
    {
        nextShootTime += Time.deltaTime;
        if(nextShootTime >= existTime)
        {
            nextShootTime = 0f;
            Destroy(gameObject);
        }
    }
    void OnTriggerStay2D(Collider2D hitinfo)
    {
        PlayerStat player = hitinfo.GetComponent<PlayerStat>();
        if (player != null)
        {
            if(nextShootTime >= chargeTime && nextShootTime<=shootTime)
            {
                player.TakeDamage1(damage);
                damage = 0;
                Debug.Log("HitPlayer");
            }
        }

    }
}
