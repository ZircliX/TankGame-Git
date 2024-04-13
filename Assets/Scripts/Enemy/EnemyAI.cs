using System;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTarget;
    public TankPrefabAccess tankAccess;
    [SerializeField] internal Shoot shoot;

    [FormerlySerializedAs("waypoints")] [Header("WayPoints")]
    public List<Transform> wayPoints;
    private int currentWaypointIndex;
    public float waypointProximityThreshold;
    
    [Header("Detection")] 
        [SerializeField] private float aimRadius, shootRadius, stopRadius;
        [SerializeField] private string playerTag;
        private bool playerInRange;
            
    private NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        ChooseWayPoint();
    }

    void Update()
    {
        AILogic();
    }

    void AILogic()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, aimRadius, transform.forward, out hit))
        {
            playerInRange = hit.collider.transform.root.gameObject.CompareTag(playerTag);
        }
        if (Physics.SphereCast(transform.position, shootRadius, transform.forward, out hit))
        {
            shoot.isShooting = hit.collider.transform.root.gameObject.CompareTag(playerTag);
        }
        if (Physics.SphereCast(transform.position, stopRadius, transform.forward, out hit))
        {
            navMeshAgent.isStopped = hit.collider.transform.root.gameObject.CompareTag(playerTag);
        }

        if (playerInRange)
        {
            LookAtTarget(playerTarget.position);
            UpdatePath(playerTarget.position);
        }
        else
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                ChooseWayPoint();
                LookAtTarget(wayPoints[currentWaypointIndex].position);
            }
        }
    }

    private void ChooseWayPoint()
    {
        currentWaypointIndex = Random.Range(0, wayPoints.Count);
        
        Vector3 targetPosition = wayPoints[currentWaypointIndex].position;

        UpdatePath(targetPosition);
    }
    
    /*private void SetRandomDestination()
    {
        // Generate a random position within the search radius
        Vector3 randomDirection = Random.insideUnitSphere * searchRadius;
        randomDirection += transform.position;

        // Find the closest valid position on the NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, searchRadius, NavMesh.AllAreas))
        {
            // Set the agent's destination to the valid position
            navMeshAgent.SetDestination(hit.position);
        }
    }*/

    private void UpdatePath(Vector3 targetPos)
    {
        navMeshAgent.SetDestination(targetPos);
    }

    private void LookAtTarget(Vector3 lookTarget)
    {
        Vector3 lookPos = lookTarget - transform.position;
        lookPos.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        tankAccess.tankTower.rotation = Quaternion.Slerp(tankAccess.tankTower.rotation, rotation, 0.2f);
    }
}