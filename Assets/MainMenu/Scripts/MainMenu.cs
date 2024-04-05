using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Menu Variables")] 
        public GameObject mainMenu;
        public GameObject pauseMenu;
        public static bool menuActive = true;
        public GameObject[] gm;
        public GameObject[] optionGm;
        
        private MenuState state = MenuState.None;
        private enum MenuState
        {
            None = -1,
            Option = 0,
            Credits = 1
        }
        
        private OptionState oState = OptionState.Tuto;
        private enum OptionState
        {
            None = -1,
            Tuto = 0,
            Control = 1,
            Video = 2,
            Audio = 3
        }

    [Header("Anim Settings")] 
        public bool animation;
        private Animator animController;

    public void OpenOptionPanel(int index)
    {
        if (oState == OptionState.None) //Open Option panel
        {
            optionGm[index].SetActive(true);
            oState = (OptionState)index;
        }
        else if ((int)oState == index) //Close Option panel
        {
            optionGm[index].SetActive(false);
            oState = OptionState.None;
        }
        else //Switch Option panel
        {
            optionGm[(int)oState].SetActive(false);
            optionGm[index].SetActive(true);
            oState = (OptionState)index;
        }
    }
    
    public void OpenPanel(int index)
    {
        //Handle the panel state change, to open / close / switch panel
        
        if (state == MenuState.None) //Open panel
        {
            gm[index].SetActive(true);
            state = (MenuState)index;
        }
        else if ((int)state == index) //Close panel
        {
            gm[index].SetActive(false);
            state = MenuState.None;
        }
        else //Switch opened panel (options -> credits)
        {
            gm[(int)state].SetActive(false);
            gm[index].SetActive(true);
            state = (MenuState)index;
        }
    }

    public void Play(GameObject panel)
    {
        //Remove the Canvas of the Main Menu
        if (state != MenuState.None)
        {
            gm[(int)state].SetActive(false);
            state = MenuState.None;
        }

        panel.SetActive(false);
        menuActive = false;
    }
    
    public void Exit()
    {
        //Close the entire game window
        Application.Quit();
    }

    public void StartAnimation(GameObject button)
    {
        animController = button.GetComponent<Animator>();
        animController.SetBool("isSelected", true);
    }

    public void StopAnimation()
    {
        animController.SetBool("isSelected", false);
    }

    public void BackToMenuNoob()
    {
        if (state != MenuState.None)
        {
            gm[(int)state].SetActive(false);
            state = MenuState.None;
        }
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        menuActive = true;
    }
}