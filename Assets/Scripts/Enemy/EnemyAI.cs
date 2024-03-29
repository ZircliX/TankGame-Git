using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public EnemyTankAccess enemyTankAccess;
    private float shootingDistance;

    private float pathUpdateDeadline;

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, enemyTankAccess.navMeshAgent.stoppingDistance);
        
        //Gizmos.DrawWireSphere(transform.position, enemyTankAccess.navMeshAgent.remainingDistance);
    }

    void Start()
    {
        shootingDistance = enemyTankAccess.navMeshAgent.stoppingDistance + 8;
    }

    void Update()
    {
        if (target != null)
        {
            bool mustMove = enemyTankAccess.navMeshAgent.remainingDistance > enemyTankAccess.navMeshAgent.stoppingDistance;

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
        Debug.Log("Shooting !");
    }

    private void UpdatePath()
    {
        if (Time.time >= pathUpdateDeadline)
        {
            pathUpdateDeadline = Time.time + enemyTankAccess.pathUpdateDelay;
            enemyTankAccess.navMeshAgent.SetDestination(target.position);
        }
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0f;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        enemyTankAccess.tankTower.rotation = Quaternion.Slerp(enemyTankAccess.tankTower.rotation, rotation, 0.2f);
    }
}
