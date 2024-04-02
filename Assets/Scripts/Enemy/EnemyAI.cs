using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public TankPrefabAccess tankAccess;
    private float shootingDistance;

    private float pathUpdateDeadline;
    [SerializeField] internal float pathUpdateTime;

    private NavMeshAgent navMeshAgent;

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, tankAccess.navMeshAgent.stoppingDistance);
        
        //Gizmos.DrawWireSphere(transform.position, tankAccess.navMeshAgent.remainingDistance);
    }

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        shootingDistance = navMeshAgent.stoppingDistance + 8;
    }

    void Update()
    {
        if (target != null)
        {
            bool mustMove = navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance;

            if (mustMove)
            {
                Shoot();
            }
            else
            {
                UpdatePath();
            }
            
            LookAtTarget();
        }
    }

    private void Shoot()
    {
        //Debug.Log("Shooting !");
    }

    private void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + pathUpdateTime;
            navMeshAgent.SetDestination(target.position);
        }
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        tankAccess.tankTower.rotation = Quaternion.Slerp(tankAccess.tankTower.rotation, rotation, 0.2f);
    }
}
