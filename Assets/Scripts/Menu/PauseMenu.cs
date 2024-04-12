using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MainMenu))]
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseCanvas;
    private MainMenu mm;

    private void Awake()
    {
        mm = gameObject.GetComponent<MainMenu>();
    }

    public void OpenPause(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed || (int)mm.state == 1) return;
        
        switch (GameManager.Instance.state)
        {
            case GameManager.GameState.GamePause:
                GameManager.Instance.SwitchState(5);
                break;
            case GameManager.GameState.LevelInProgress:
                GameManager.Instance.SwitchState(1);
                break;
        }
        
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);
    }

    public void ResumeGame()
    {
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);
        GameManager.Instance.SwitchState(5);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}