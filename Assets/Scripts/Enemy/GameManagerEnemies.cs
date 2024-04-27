using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerEnemies : MonoBehaviour
{
    private int enemyCount;

    private static event Action EnemyKilled;

    public static void InvokeEnemyKilled()
    {
        EnemyKilled?.Invoke();
    }

    private void Start()
    {
        EnemyKilled += RefreshEnemyNumber;
        enemyCount = SceneManager.GetActiveScene().buildIndex - 1;
    }

    private void RefreshEnemyNumber()
    {
        enemyCount--;
        
        //The condition turns true even if the enemyCount IS NOT 0..
        //The line "GameManager.Instance.SwitchState(6);" is only written once throughout the entire code
        //I don't understand why, I've been stuck here for multiples hours
        if (enemyCount < 1 )
        {
            //Win condition
            GameManager.Instance.SwitchState(6);
        }
    }
}
