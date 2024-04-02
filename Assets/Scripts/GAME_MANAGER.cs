using System;
using UnityEngine;

public class GAME_MANAGER : MonoBehaviour
{
    public static GAME_MANAGER Instance;

    public GameObject playerTank;
    public MENU_MANAGER menuManager;

    public GameState state;
    public enum GameState
    {
        Menu,
        Started,
        Paused,
        PlayerDead,
        EnemiesDead,
        GameFinished
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.Menu:
                break;
            case GameState.Started:
                break;
            case GameState.Paused:
                break;
            case GameState.PlayerDead:
                HandlePlayerDead();
                break;
            case GameState.EnemiesDead:
                break;
            case GameState.GameFinished:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    private void HandlePlayerDead()
    {
        playerTank.GetComponent<TankScript>().enabled = false;
        playerTank.GetComponent<IDamagable>().enabled = false;
        //ts.cameraTargetPosition = killedByEnemy.cameraPosition;

        MENU_MANAGER.Instance.playerDead.SetActive(true);
    }

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.Started);
    }
}