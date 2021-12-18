using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Assassin : MonoBehaviour
{
    NavMeshAgent thisNavMeshAgent;
    public bool playerInRange;
    public Transform player;
    public LayerMask isGround, isPlayer;

    //range to chat with player
    public float sightRange;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        thisNavMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
    }
}
