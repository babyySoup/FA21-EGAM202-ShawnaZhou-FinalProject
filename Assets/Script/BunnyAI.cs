using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BunnyAI : MonoBehaviour
{
    public enum BunnyStateT
    {
        DecidingWhatToDoNext,
        SeekingWater, MovingToWater, DrinkingTillFull, Resting, SeekingFood, MovingToFood, EatingTillFull, Reproduce, 
    }

    public float Water = 50, WaterLostPS = 1, DrinkingSpeed = 10;
    public float Food = 50, FoodLostPS = 1, EatingSpeed = 10;

    public BunnyStateT currentState;

    //for reproducing
    public GameObject BunnyPrefab;
    public float CrowdingDistance, MaxDispersalDistance;

    NavMeshAgent thisNavMeshAgent;
    //!!


    // Start is called before the first frame update
    void Start()
    {
        Water = Random.Range(40f, 70f);
        Food = Random.Range(40f, 70f);
        thisNavMeshAgent = GetComponent<NavMeshAgent>();

        currentState = BunnyStateT.DecidingWhatToDoNext;
    }

    // Update is called once per frame
    void Update()
    {
        Water -= WaterLostPS * Time.deltaTime;
        Food -= FoodLostPS * Time.deltaTime; 


        if (Food < 0 || Water < 0)
        {
            Destroy(this.gameObject);
        }

        switch (currentState)
        {
            case BunnyStateT.DecidingWhatToDoNext:
                DecideWhatToDoNext();
                break;
            case BunnyStateT.SeekingWater:
                SeekWater();
                break;
            case BunnyStateT.MovingToWater:
                break;
            case BunnyStateT.DrinkingTillFull:
                DrinkFromWater();
                break;
            case BunnyStateT.SeekingFood:
                SeekFood();
                break;
            case BunnyStateT.MovingToFood:
                break;
            case BunnyStateT.EatingTillFull:
                EatMushrooms();
                break;
            case BunnyStateT.Reproduce:
                MakeBunny();
                break;


        }
    }

    public void DecideWhatToDoNext()
    {
        if (Water < 50)
        {
            currentState = BunnyStateT.SeekingWater;
            return;
        }
        if (Food < 50)
        {
            currentState = BunnyStateT.SeekingFood;
        }
        if (Food > 30 && Water > 80)
        {
            currentState = BunnyStateT.Reproduce;
        }
    }

    public void SeekWater()
    {
        GameObject[] wateryObjects = GameObject.FindGameObjectsWithTag("Water");
        GameObject targetWateryObject = wateryObjects[0];
        Debug.Log("Bunny is going to " + targetWateryObject.name);

        thisNavMeshAgent.SetDestination(targetWateryObject.transform.position);
        currentState = BunnyStateT.MovingToWater;
    }


    public void DrinkFromWater()
    {
        Water += DrinkingSpeed * Time.deltaTime;

        if (Water > 100)
            currentState = BunnyStateT.DecidingWhatToDoNext;
    }

    //food

    public void SeekFood()
    {
        GameObject[] MushroomObjects = GameObject.FindGameObjectsWithTag("ShroomPlant");
        GameObject targetMushroomObject = MushroomObjects[0];
        Debug.Log("Bunny is going to " + targetMushroomObject.name);

        thisNavMeshAgent.SetDestination(targetMushroomObject.transform.position);
        currentState = BunnyStateT.MovingToFood;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentState == BunnyStateT.MovingToWater && other.tag == "Water")
        {
            currentState = BunnyStateT.DrinkingTillFull;
        }
        if (currentState == BunnyStateT.MovingToFood && other.tag == "ShroomPlant")
        {
            currentState = BunnyStateT.EatingTillFull;
        }

    }

    public void EatMushrooms()
    {
        Food += EatingSpeed * Time.deltaTime;

        if (Food > 100)
            currentState = BunnyStateT.DecidingWhatToDoNext;
    }

    
    //reproduce 
    public void MakeBunny()
    {
        Vector3 randomNearbyPosition;
        randomNearbyPosition = transform.position + MaxDispersalDistance * Random.insideUnitSphere;

        Instantiate(BunnyPrefab, randomNearbyPosition, Quaternion.identity, transform.parent);

        Food -= 20;
        Water -= 50;
    }




    //find close 
    public GameObject FindClosestObjectWithTag (string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        if (objectsWithTag.Length == 0)
            return null;

        GameObject closestObject = objectsWithTag[0];
        float distanceToClosestObject = 1e6f,
            distanceToCurrentObject;
        for (int i = 0; i < objectsWithTag.Length; i++)
        {
            Vector3 vectorToCurrentObject;
            GameObject currentObject;
            currentObject = objectsWithTag[i];
            vectorToCurrentObject = currentObject.transform.position - transform.position;
            distanceToCurrentObject = vectorToCurrentObject.magnitude;
            if (distanceToCurrentObject < distanceToClosestObject)
            {
                closestObject = objectsWithTag[i];
                distanceToClosestObject = distanceToCurrentObject;
            }
        }
        return closestObject;
    }

}
