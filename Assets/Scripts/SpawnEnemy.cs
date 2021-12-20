using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject spawnCreature;
    public bool stopSpawning = false;
    public float spawnTime;
    //time in between
    public float spawnDuration; 

    void Start()
    {
        InvokeRepeating("SpawnCreature", spawnTime, spawnDuration);
    }

    public void SpawnCreature()
    {
        Instantiate(spawnCreature, transform.position, transform.rotation);
        if (stopSpawning)
        {
            CancelInvoke("SpawnCreature");
        }
    }
}
