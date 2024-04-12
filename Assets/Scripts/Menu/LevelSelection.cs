using Michsky.MUIP;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public ButtonManager[] lvlButtons;
    
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 0);

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i >= levelAt) continue;
            
            //Switch from locked to enabled
            lvlButtons[i].Interactable(true);
            lvlButtons[i].enableIcon = false;
            lvlButtons[i].enableText = true;
            
            lvlButtons[i].SetText((i + 1).ToString());
            lvlButtons[i].UpdateUI();
        }
    }

    public void LoadLevel(int index)
    {
        GameManager.InvokeStateChange(5);
        SceneManager.LoadScene(index+1);
    }
    
    public void GoBack(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Performed) return;

        SceneManager.LoadScene(0);
    }
}