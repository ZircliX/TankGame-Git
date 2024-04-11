using System;
using UnityEngine;

public class EnemyTankManager : MonoBehaviour
{
    [SerializeField] private int tankNumber;
    private int tanks;
    
    private static event Action EnemyKilled;

    public static void InvokeEnemyKilled()
    {
        EnemyKilled?.Invoke();
    }

    private void Start()
    {
        EnemyKilled += RefreshEnemyNumber;
        tanks = tankNumber;
    }

    private void RefreshEnemyNumber()
    {
        tanks -= 1;
        
        if (tanks == 0)
        {
            GameManager.InvokeStateChange(6);
        }
    }
}