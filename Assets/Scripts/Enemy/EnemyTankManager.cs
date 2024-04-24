using System;
using UnityEngine;

public class EnemyTankManager : MonoBehaviour
{
    [SerializeField] private int tankNumber;

    private static event Action EnemyKilled;

    public static void InvokeEnemyKilled()
    {
        EnemyKilled?.Invoke();
    }

    private void Start()
    {
        EnemyKilled += RefreshEnemyNumber;
    }

    private void RefreshEnemyNumber()
    {
        tankNumber -= 1;
        
        if (tankNumber == 0)
        {
            GameManager.InvokeStateChange(6);
        }
    }
}