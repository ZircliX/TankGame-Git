using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject[] player;
    
    public GameState state = GameState.LevelStarted;
    public enum GameState
    {
        LevelStarted,
        LevelFinished,
        PlayerDead
    }

    private void Update()
    {
        CheckGameChange();
    }

    public void CheckGameChange()
    {
        if (enemies.Length == 0)
        {
            Debug.Log("Win !");
            state = GameState.LevelFinished;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (player.Length == 0)
        {
            Debug.Log("Lost !");
            state = GameState.PlayerDead;
            //open canvas...
        }
    }
    
    public void HandleReset(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}