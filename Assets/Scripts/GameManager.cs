using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float spawnRate;
    float timer;

    public GameObject creaturePrefab;
    bool gameStart;
    
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        gameStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            timer += Time.deltaTime;
        }

        if(timer > 1/ spawnRate)
        {
            timer = 0;
            //more enemy as time goes up 
            spawnRate *= 1.001f;
            SpawnCreature();

        }
    }

    void SpawnCreature()
    {
        transform.Rotate(Vector3.up, Random.Range(0, 350f));
        Vector3 spawnPos = transform.forward * Random.Range(20, 25f);

        GameObject creature = Instantiate(creaturePrefab, spawnPos, Quaternion.identity);
        Destroy(creature, 30f);
    }
}
