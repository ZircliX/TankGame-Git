using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPauseActive;
    
    public void HandlePause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            isPauseActive = !isPauseActive;
            pauseMenu.SetActive(isPauseActive);
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