using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public GameState state = GameState.Menu;
    public enum GameState
    {
        Menu,
        LevelStarted,
        LevelFinished,
        PlayerDead
    }
    
    public void HandleReset(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}