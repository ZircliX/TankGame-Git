using UnityEngine;
using UnityEngine.InputSystem;

public class OpenPause : MonoBehaviour
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
}