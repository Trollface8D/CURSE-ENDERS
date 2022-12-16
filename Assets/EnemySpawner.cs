using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Kobold;
    [SerializeField]
    private GameObject Snail;
    [SerializeField]
    private GameObject Grim;

    public float KoboldInterval=4f;
    public float SnailInterval=5f;
    public float GrimInterval=20f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(KoboldInterval, Kobold));
        StartCoroutine(spawnEnemy(SnailInterval, Snail));
        StartCoroutine(spawnEnemy(GrimInterval,Grim));
    }

    private IEnumerator spawnEnemy(float interval,GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, transform.position, transform.rotation);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
