using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wavescript : MonoBehaviour
{
    Text wave;

    
    void Start()
    {
        wave = GetComponent<Text> ();
    }

    void Update()
    {
        wave.text = "wave "+ EnemySpawner.Wave;
    }
}
