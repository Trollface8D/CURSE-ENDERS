using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float ShootRate = 1f;
    private float nextShootTime = 0f;
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<EnemyAI>().onChase)
        {
            nextShootTime += Time.deltaTime;
            if (nextShootTime >= ShootRate && !GetComponent<EnemyStat>().staggered)
            {
                EnemyShoot();
                nextShootTime = 0f;
            }
        }
        else
        {
            nextShootTime = 0f;
        }
    }
    void EnemyShoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
