using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreatureS : MonoBehaviour
{
    //states
    public enum EnemyStateT
    {
        DecidingWhatToDoNext,
        SeekingPlayer, MovingToPlayer, Attack, Death
    }
    public EnemyStateT currentState;

    NavMeshAgent thisNavMeshAgent;
    public float moveSd = 15f;
    public int Damage = 20;
    public Transform player;

    //enemy Ai stats
    public float health = 1f;
    public float sightRange;
    public float attackRange;
    public bool playerInSight;
    public bool playerInAttackR;
    public LayerMask isGround, isPlayer;


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


        //enemy death addressed in bullet script
        //this enemy is fast and chase soon as ure exposed 
        //very fast, but die in one shot

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
            case EnemyStateT.Death:
                Die();
                break;
        }
    }



    public void DecideWhatToDoNext()
    {
        if (playerInSight && !playerInAttackR)
        {
            currentState = EnemyStateT.SeekingPlayer;
            return;
        }
        if (playerInSight && playerInAttackR)
        {
            currentState = EnemyStateT.Attack;
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
        currentState = EnemyStateT.Death;
    }

    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        currentState = EnemyStateT.Death;
    //    }
    //}


    private void OnTriggerStay(Collider other)
    {
        if (currentState == EnemyStateT.MovingToPlayer && other.tag == "Player")
        {
            currentState = EnemyStateT.Attack;
        }

    }


    public void ChasePlayer()
    {
        thisNavMeshAgent.SetDestination(player.transform.position);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
}
