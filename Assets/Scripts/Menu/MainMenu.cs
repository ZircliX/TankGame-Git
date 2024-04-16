using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] panelList;
    private MenuState state = MenuState.Menu;
    private enum MenuState
    {
        Menu = 0,
        Options = 1
    }

    public GameObject defaultSelectMenu;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultSelectMenu);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OpenPanel(int index)
    {
        if (index != (int)state) //Switch panel
        {
            panelList[(int)state].SetActive(false);
            panelList[index].SetActive(true);
            state = (MenuState)index;
        }
    }

    public void GoBack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (state == MenuState.Options)
        {
            OpenPanel(0);
        }
    }
}
