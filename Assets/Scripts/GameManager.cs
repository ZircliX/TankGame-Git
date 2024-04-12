using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState state = GameState.InMenu;
    public enum GameState
    {
        InMenu = 0,
        GamePause = 1,
        LevelInProgress = 5,
        LevelFinished = 6,
        PlayerDead = 10
    }

    private static event Action<int> ChangeState;

    public static void InvokeStateChange(int stateIndex)
    {
        ChangeState?.Invoke(stateIndex);
    }

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Optionally, find the GameManager object in the scene, if it's not already set.
                _instance = FindAnyObjectByType<GameManager>();
                if (_instance == null)
                {
                    // Create a new GameObject with a GameManager component if none exists.
                    GameObject gameManager = new GameObject("GameManager");
                    _instance = gameManager.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // Ensure there's only one instance by destroying duplicates.
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Optionally, make the GameManager persist across scenes.
        }
    }

    private void Start()
    {
        ChangeState += SwitchState;
    }

    private void SwitchState(int newState)
    {
        state = (GameState)newState;
        CheckGameChange();
    }

    private void CheckGameChange()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        
        switch (state)
        {
            case GameState.LevelInProgress:
                Debug.Log("Game started / resumed !");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            
            case GameState.LevelFinished:
                Debug.Log("Enemies killed go next !");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                SwitchState(5); // To remove later
                if (PlayerPrefs.GetInt("levelAt", 0) >= SceneManager.GetActiveScene().buildIndex)
                    PlayerPrefs.SetInt("levelAt", SceneManager.GetActiveScene().buildIndex);
                break;
            
            case GameState.PlayerDead:
                Debug.Log("Player dead go menu !");
                SceneManager.LoadScene(0);
                break;
            
            case GameState.InMenu:
                Debug.Log("Game is in menu !");
                break;
            
            case GameState.GamePause:
                Debug.Log("Game is paused !");
                Time.timeScale = 0f;
                break;
        }
    }
    
    public void HandleReset(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void HandlePause(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;
        
        switch (state)
        {
            case GameState.GamePause:
                SwitchState(5);
                break;
            case GameState.LevelInProgress:
                SwitchState(1);
                break;
        }
    }
}