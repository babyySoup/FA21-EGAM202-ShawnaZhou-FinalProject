using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    //states
    public enum EnemyStateT
    {
        DecidingWhatToDoNext,
        SeekingPlayer, MovingToPlayer, Attack, Escape, Heal
    }
    public EnemyStateT currentState;

    NavMeshAgent thisNavMeshAgent;
    public float moveSd = 10f;
    public float health;
    public int Damage = 5;
    public Transform player;

    //enemy Ai stats
    public float sightRange;
    public float attackRange;
    public bool playerInSight;
    public bool playerInAttackR;
    public LayerMask isGround, isPlayer;
    public float Hunger = 50f, HungerLostPS = 0.5f;
    public float HPGain = 3f;


    void Start()
    {
        player = GameObject.Find("Player").transform;
        thisNavMeshAgent = GetComponent<NavMeshAgent>();
        currentState = EnemyStateT.DecidingWhatToDoNext;
    }

    // Update is called once per frame
    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackR = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        Hunger -= HungerLostPS * Time.deltaTime;

        if (health < 1)
        {
            Destroy(this.gameObject);
        }

        if (health <= 30 && currentState != EnemyStateT.Heal)
        {
            currentState = EnemyStateT.Escape;
        }

            //enemy death addressed in bullet script

            switch (currentState)
        {
            case EnemyStateT.DecidingWhatToDoNext:
                DecideWhatToDoNext();
                break;
            case EnemyStateT.SeekingPlayer:
                SeekPlayer();
                break;
            case EnemyStateT.MovingToPlayer:
                ChasePlayer();
                break;
            case EnemyStateT.Attack:
                AttackPlayer();
                break;
            case EnemyStateT.Escape:
                Run();
                break;
            case EnemyStateT.Heal:
                HealSelf();
                break;

        }
    }



    public void DecideWhatToDoNext()
    {
        if (health >= 50)
        {
            if (playerInSight && !playerInAttackR && Hunger < 45)
            {
                currentState = EnemyStateT.SeekingPlayer;
                return;
            }
            if (playerInSight && playerInAttackR && Hunger < 45)
            {
                currentState = EnemyStateT.Attack;
                return;
            }
        }

        if (health <= 30)
        {
            currentState = EnemyStateT.Escape;
            return;
        }
    }

    public void SeekPlayer()
    {
        GameObject[] playerAvatar = GameObject.FindGameObjectsWithTag("Player");
        GameObject targetPlayerObject = playerAvatar[0];


        thisNavMeshAgent.SetDestination(targetPlayerObject.transform.position);
        currentState = EnemyStateT.MovingToPlayer;
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);
        //damage code is written in player controller script
        if (health <= 30)
        {
            currentState = EnemyStateT.Escape;
        }
        if (Hunger < 10)
        {
            currentState = EnemyStateT.DecidingWhatToDoNext;
        }
    } 
    
    //escaping and safe spot

    public void Run()
    {
        GameObject[] SafeObjects = GameObject.FindGameObjectsWithTag("SafeSpot");
        if(SafeObjects.Length > 0)
        {
            GameObject targetSafeObject = SafeObjects[0];
            thisNavMeshAgent.SetDestination(targetSafeObject.transform.position);
        }
    }

    public void HealSelf()
    {
        health += HPGain * Time.deltaTime;

        if (health >= 50)
        {
            currentState = EnemyStateT.DecidingWhatToDoNext;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (currentState == EnemyStateT.MovingToPlayer && other.tag == "Player")
        {
            currentState = EnemyStateT.Attack;
        }
        ////heal when touching safe spot 
        if(other.tag == "SafeSpot")
        {
            if (currentState == EnemyStateT.Escape)
            {
                Debug.Log("Running");
                currentState = EnemyStateT.Heal;
            }
        }
    }


    public void ChasePlayer()
    {
        thisNavMeshAgent.SetDestination(player.transform.position);
    }


    public GameObject FindClosestObjectWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        if (objectsWithTag.Length == 0)
        {
            return null;
        }
        

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

