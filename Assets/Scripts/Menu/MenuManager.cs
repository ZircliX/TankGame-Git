using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject[] panelList;
    [SerializeField] private GameObject[] defaultSelected;

    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private Slider[] audioSliders;
    
    private MenuState state = MenuState.Menu;
    private int lastStateIndex;
    private enum MenuState
    {
        None = -1,
        Menu = 0,
        Options = 1,
        Pause = 2,
        Won = 3,
        Lost = 4,
        LevelSelection = 10
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

        audioSources[0].volume = PlayerPrefs.GetFloat("Volume0", 0.5f);
        audioSources[1].volume = PlayerPrefs.GetFloat("Volume1", 0.5f);
        audioSliders[0].value = PlayerPrefs.GetFloat("Volume0", 0.5f);
        audioSliders[1].value = PlayerPrefs.GetFloat("Volume1", 0.5f);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        SwitchState(10);
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
                GameManager.InvokeStateChange(0);
                break;
            case MenuState.Options:
                break;
            case MenuState.Pause:
                break;
            case MenuState.Lost:
                break;
            case MenuState.Won:
                break;
            case MenuState.None:
                GameManager.InvokeStateChange(5);
                return;
            case MenuState.LevelSelection:
                GameManager.InvokeStateChange(0);
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
            SwitchState(lastStateIndex);
        }
    }

    public void UpdateSound(int index)
    {
        audioSources[index].volume = audioSliders[index].value;
        PlayerPrefs.SetFloat("Volume" + index, audioSliders[index].value);
        
        //AudioManager.Instance.PlaySFX("Hover");
    }
    
    public void Retry()
    {
        SwitchState(-1);
        GameManager.InvokeStateChange(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void Next()
    {
        if (SceneManager.GetActiveScene().buildIndex == 7) return;
        
        SwitchState(-1);
        GameManager.InvokeStateChange(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}