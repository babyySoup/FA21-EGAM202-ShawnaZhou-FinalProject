using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground, Player;

    public float creatureHealth;

    public enum EnemyStateT
    {
        DecidingWhatToDo, Patrolling, Chasing, Attacking
    }

    public EnemyStateT currentState;

    //patrol 
    public Vector3 guardPoint;
    bool guarding;
    public float guardPointRange;

    //attacking
    public float attackCD;
    bool attacked;

    //state machine stats
    public float alarmRange;
    public float attackRange;
    public bool playerInAlarmRange, playerInAttackRange;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        currentState = EnemyStateT.DecidingWhatToDo;
    }


    private void Update()
    {
        //enemy checking for range
        playerInAlarmRange = Physics.CheckSphere(transform.position, alarmRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        switch (currentState)
        {
            case EnemyStateT.DecidingWhatToDo:
                Decide();
                break;
            case EnemyStateT.Patrolling:
                Patrol();
                break;
            case EnemyStateT.Chasing:
                ChasePlayer();
                break;
            case EnemyStateT.Attacking:
                AttackPlayer();
                break;
                    
        }
    }


    public void Decide()
    {
        if (!playerInAlarmRange && !playerInAttackRange)
        {
            currentState = EnemyStateT.Patrolling;
        }
        if (playerInAlarmRange && !playerInAttackRange)
        {
            currentState = EnemyStateT.Chasing;
        }
        if (playerInAttackRange && playerInAlarmRange)
        {
            currentState = EnemyStateT.Attacking;
        }
    }
    private void Patrol()
    {
        if (!guarding)
        {
            Search();
        }
        if (guarding)
        {
            agent.SetDestination(guardPoint);
        }

        Vector3 distance = transform.position - guardPoint;
        if (distance.magnitude < 1f)
        {
            guarding = false;
        }

        currentState = EnemyStateT.DecidingWhatToDo;

    }

    private void Search()
    {
        //set random place to guard
        float Z = Random.Range(-guardPointRange, guardPointRange);
        float X = Random.Range(-guardPointRange, guardPointRange);

        guardPoint = new Vector3(transform.position.x + X, transform.position.y, transform.position.z + Z);
        //check if it is on map using Raycast
        if (Physics.Raycast(guardPoint, -transform.up, 2f, Ground))
        {
            guarding = true;
        }
    }


    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        currentState = EnemyStateT.DecidingWhatToDo;

    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        //transform.LookAt(player);

        if (!attacked)
        {
            //attack code


            attacked = true;
            Invoke(nameof(CoolDown), attackCD);
        }
        currentState = EnemyStateT.DecidingWhatToDo;
    }

    private void CoolDown()
    {
        attacked = false;
    }

    //getting hit
    public void TakeDamage(int damage)
    {
        creatureHealth -= damage;
        if (creatureHealth <= 0)
        {
            Destroy(gameObject);
        }
    }




}
