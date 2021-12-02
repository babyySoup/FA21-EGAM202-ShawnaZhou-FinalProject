using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creature : MonoBehaviour
{
    public NavMeshAgent agent;
    public float moveSd = 5f;
    public float health;
    public int Damage = 5;
    Rigidbody rb;
    public Transform player;

    //enemy Ai stats
    public float sightRange;
    public float attackRange;
    public bool playerInSight;
    public bool playerInAttackR;
    public LayerMask isGround, isPlayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //transform.LookAt(Vector3.zero);
        //rb.velocity = transform.forward * moveSd;
    }

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackR = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if(playerInSight && !playerInAttackR)
        {
            ChasePlayer();
        }
        if(playerInSight && playerInAttackR)
        {
            AttackPlayer();
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }
}
