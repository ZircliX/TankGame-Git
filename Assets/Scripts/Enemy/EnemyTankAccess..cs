using UnityEngine;
using UnityEngine.AI;

public class EnemyTankAccess : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform tankBase;
    public Transform tankTower;
    public Transform[] shootPoints;
    public Transform cameraPos;

    public float pathUpdateDelay;
}