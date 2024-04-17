using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panelList;
    [SerializeField] private GameObject[] defaultSelected;
    
    private MenuState state = MenuState.Menu;
    private int lastStateIndex;
    private enum MenuState
    {
        None = -1,
        Menu = 0,
        Options = 1,
        Pause = 2,
        LevelSelection = 3
    }
    
    private static MenuManager _instance;
    public static MenuManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Optionally, find the GameManager object in the scene, if it's not already set.
                _instance = FindAnyObjectByType<MenuManager>();
                if (_instance == null)
                {
                    // Create a new GameObject with a GameManager component if none exists.
                    GameObject menuManager = new GameObject("MenuManager");
                    _instance = menuManager.AddComponent<MenuManager>();
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
        CheckStateChange();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        SwitchState(3);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SwitchState(0);
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SwitchState(int newState)
    {
        lastStateIndex = (int)state;
        state = (MenuState)newState;
        CheckStateChange();
    }

    private void CheckStateChange()
    {
        foreach (GameObject panel in panelList)
        {
            panel.SetActive(false);
        }

        switch (state)
        {
            case MenuState.Menu:
                break;
            case MenuState.Options:
                break;
            case MenuState.Pause:
                break;
            case MenuState.None:
                GameManager.InvokeStateChange(5);
                return;
            case MenuState.LevelSelection:
                return;
        }
        
        panelList[(int)state].SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(defaultSelected[(int)state]);
    }

    public void OpenPause(InputAction.CallbackContext context)
    {
        if (!context.performed || GameManager.Instance.state != GameManager.GameState.LevelInProgress) return;
        
        SwitchState(2);
        GameManager.InvokeStateChange(1);
    }

    public void GoBack(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        if (state == MenuState.Options)
        {
            ExitOptions();
        }
    }

    public void ExitOptions()
    {
        SwitchState(lastStateIndex);
    }
}
