using Michsky.MUIP;
using UnityEngine;
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
            lvlButtons[i].Interactable(true);
            lvlButtons[i].enableIcon = false;
            lvlButtons[i].enableText = true;
            lvlButtons[i].SetText(i.ToString());
            lvlButtons[i].UpdateUI();
        }
    }

    public void LoadLevel(int index)
    {
        GameManager.InvokeStateChange(5);
        SceneManager.LoadScene(index+1);
    }
}