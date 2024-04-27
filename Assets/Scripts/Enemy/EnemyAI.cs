using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTarget;
    public TankPrefabAccess tankAccess;
    [SerializeField] internal Shoot shoot;

    private Vector3 lookPosition;

    [Header("Detection")] 
        [SerializeField] private float searchRadius;
        [SerializeField] private Collider shootDistanceCollider;
        [SerializeField] private Collider stopDistanceCollider;
        [SerializeField] private Collider aimDistanceCollider;
        private bool playerInRange;
            
    private NavMeshAgent navMeshAgent;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        SetRandomDestination();
    }

    void Update()
    {
        AILogic();
    }

    void AILogic()
    {
        playerInRange = aimDistanceCollider.bounds.Contains(playerTarget.position);
        
        shoot.isShooting = shootDistanceCollider.bounds.Contains(playerTarget.position);
        //Animation of Shooting
        tankAccess.animator.SetBool("isShooting", shoot.isShooting && shoot.canShoot);
        
        navMeshAgent.isStopped = stopDistanceCollider.bounds.Contains(playerTarget.position);
        
        if (playerInRange)
        {
            navMeshAgent.SetDestination(playerTarget.position);
            lookPosition = playerTarget.position;
        }
        else
        {
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                SetRandomDestination();
            }
        }
        
        LookAtTarget(lookPosition);
    }
    
    private void SetRandomDestination()
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
            lookPosition = hit.position;
        }
    }

    private void LookAtTarget(Vector3 targetPos)
    {
        targetPos -= transform.position;
        targetPos.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(targetPos);
        tankAccess.tankTower.rotation = Quaternion.Slerp(tankAccess.tankTower.rotation, rotation, 10f * Time.deltaTime);
    }
}