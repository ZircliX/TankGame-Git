using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;
    
    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 0);

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i > levelAt)
            {
                lvlButtons[i].interactable = false;
            }
        }
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index+1);
    }
}
