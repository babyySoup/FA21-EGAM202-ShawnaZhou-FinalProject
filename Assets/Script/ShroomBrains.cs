using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomBrains : MonoBehaviour
{
    public enum ShroomFieldStateT { Seed, Seedling, Adult, Mutate, Death, }
    public ShroomFieldStateT currentState;
    public int ShroomFieldID;
    public float Food, SeedFood, FoodGainedPerSecond, MaxFood;
    public float CrowdingDistance, MaxDispersalDistance;
    public float BirthTime, Age;

    public GameObject ShroomPrefab;

    private Collider[] shroomfieldHits;

    void Start()
    {
        float terrianHeight = GameObject.Find("Terrain").GetComponent<Terrain>().SampleHeight(transform.position);
        transform.position = new Vector3(transform.position.x, terrianHeight, transform.position.z);
        shroomfieldHits = Physics.OverlapSphere(transform.position, CrowdingDistance, LayerMask.GetMask("ShroomPlant"));

        if (shroomfieldHits.Length > 1)
        {
            gameObject.SetActive(false);
            Object.Destroy(this.gameObject);
            return;
        }
        BirthTime = Time.time;
        Age = 0;
        currentState = ShroomFieldStateT.Seed;
        transform.localScale = new Vector3(1, 1, 1);
        
        Food = SeedFood;
    }
    

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case ShroomFieldStateT.Seed:
                SeedUpdate();
                break;
            case ShroomFieldStateT.Seedling:
                SeedlingUpdate();
                break;
            case ShroomFieldStateT.Adult:
                AdultUpdate();
                break;
            case ShroomFieldStateT.Mutate:
                MutateUpdate();
                break;
            case ShroomFieldStateT.Death:
                DeathUpdate();
                break;

        }
    }

    public void SeedUpdate()
    {
        Food += FoodGainedPerSecond * Time.deltaTime;
        Age = Time.time - BirthTime;

        if (Age > 5)
        {
            transform.localScale = new Vector3(1, 2, 1);
            currentState = ShroomFieldStateT.Seedling;
        }
    }

    public void SeedlingUpdate()
    {
        Food += FoodGainedPerSecond * Time.deltaTime;
        Age = Time.time - BirthTime;

        if (Age > 10)
        {
            transform.localScale = new Vector3(1, 4, 1);
            currentState = ShroomFieldStateT.Adult;
        }
    }


    public void AdultUpdate()
    {
        Food += FoodGainedPerSecond * Time.deltaTime;
        Age = Time.time - BirthTime;

        if (Food>MaxFood)
        {
            Vector3 randomNearbyPosition;
            randomNearbyPosition = transform.position + MaxDispersalDistance * Random.insideUnitSphere;

            Instantiate(ShroomPrefab, randomNearbyPosition, Quaternion.identity, transform.parent);

            Food -= 2f * SeedFood;
        }

        if (Age > 30)
        {
            currentState = ShroomFieldStateT.Mutate;
        }


    }

    public void MutateUpdate()
    {
        Food += FoodGainedPerSecond * Time.deltaTime;
        Age = Time.time - BirthTime;

        Color newColor = Color.red;
        GetComponent<Renderer>().material.color = newColor;

        if (Age > 60)
        {
            currentState = ShroomFieldStateT.Death;
        }

    }


    public void DeathUpdate()
    {
        Destroy(this.gameObject);
    }

}
