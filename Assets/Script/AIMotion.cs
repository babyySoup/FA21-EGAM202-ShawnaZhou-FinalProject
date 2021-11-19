using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMotion : MonoBehaviour
{
    public enum MotionTypeT { NA, WaypointPatrol, RandomWalk }
    public MotionTypeT MotionType;

    public Transform[] Waypoints;
    public int CurrentDestinationIndex;

    //random walk
    public GameObject DestinationMarker;
    public float MaxStepSize;
    private NavMeshAgent thisNavMeshAgent;

    
   
    // Start is called before the first frame update
    void Start()
    {
        thisNavMeshAgent = GetComponent<NavMeshAgent>();

        switch (MotionType)
        {
            case MotionTypeT.WaypointPatrol:
                CurrentDestinationIndex = 0;
                thisNavMeshAgent.SetDestination(Waypoints[0].position);
                break;
            case MotionTypeT.RandomWalk:
                DestinationMarker = new GameObject();
                DestinationMarker.name = "DestinationMarker for " + name;

                Vector3 randomStep = MaxStepSize * Random.onUnitSphere;
                DestinationMarker.transform.position = transform.position + randomStep;
                thisNavMeshAgent.SetDestination(DestinationMarker.transform.position);
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (MotionType)
        {
            case MotionTypeT.WaypointPatrol:
                WaypointPatrolMotion();
                break;
            case MotionTypeT.RandomWalk:
                RandomWalkMotion();
                break;
        }
    }

    private void WaypointPatrolMotion()
    {

        if (thisNavMeshAgent.remainingDistance < 0.5f)
        {
            CurrentDestinationIndex++;
            if (CurrentDestinationIndex == Waypoints.Length)
                CurrentDestinationIndex = 0;

            thisNavMeshAgent.SetDestination(Waypoints[CurrentDestinationIndex].position);

        }
    }
    private void RandomWalkMotion()
    {
        bool asCloseAsPossible = false;
        if (thisNavMeshAgent.velocity.magnitude < 0.1f)
            asCloseAsPossible = true;
        if (asCloseAsPossible)
        {
            Debug.Log("Go somewhere else");
            Vector3 randomStep = MaxStepSize * Random.onUnitSphere;
            DestinationMarker.transform.position = transform.position + randomStep;
            thisNavMeshAgent.SetDestination(DestinationMarker.transform.position);
        }
    }
}
