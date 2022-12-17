using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    private float ShootRate = 1f;
    private float nextShootTime = 0f;

    [SerializeField] private AudioSource snailshootsoundeff;
    
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
        snailshootsoundeff.Play();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
