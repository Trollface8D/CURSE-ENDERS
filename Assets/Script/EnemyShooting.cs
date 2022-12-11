using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float ShootRate = 0.5f;
    private float nextShootTime = 0f;
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemyAI>().onChase)
        {
            if (Time.time >= nextShootTime && !GetComponent<EnemyStat>().staggered)
            {
                EnemyShoot();
                nextShootTime = Time.time + 1f / ShootRate;
            }
        }
    }
    void EnemyShoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
