using UnityEngine;
using UnityEngine.InputSystem;

public class OpenPause : MonoBehaviour
{
    public GameObject pauseMenu;
    
    public void HandlePause(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            pauseMenu.SetActive(true);
        }
    }
}