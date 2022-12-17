using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;

    public Animator animator;

    private float ShootRate = 1f;
    private float nextShootTime = 0f;

    [SerializeField] private AudioSource snailshootsoundeff;
    
    void Update()
    {
        if (GetComponent<EnemyAI>().onChase)
        {
            animator.SetBool("Shoot", false);
            nextShootTime += Time.deltaTime;
            if (nextShootTime >= ShootRate && !GetComponent<EnemyStat>().staggered)
            {
                animator.SetBool("Shoot",true);
                EnemyShoot();
                nextShootTime = 0f;
            }
        }
        else
        {
            animator.SetBool("Shoot", false);
            nextShootTime = 0f;
        }
    }
    void EnemyShoot()
    {   
        snailshootsoundeff.Play();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
