using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    public void Resume()
    {
        pausePanel.SetActive(false);
        GameManager.InvokeStateChange(5);
    }

    public void GoMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenPause(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        pausePanel.SetActive(true);
    }
}
