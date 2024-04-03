using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
   public static void GoToGame()
   {
        SceneManager.LoadScene(2);
   }

   public static void LevelUp()
   {
        if (SceneManager.GetActiveScene().buildIndex == 14){ // max Level
            Debug.Log("You are at the last level");
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
   }

   public static void RetryLevel()
   {
     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   public static void BackToMenu()
   {
        SceneManager.LoadScene("Menu");
        Cursor.visible = true;

        Time.timeScale = 1f;
   }

   public void BakaSable()
   {
        SceneManager.LoadScene("BakaSable");
        Cursor.visible = false;
   }

   public void Level(int nbr)
   {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + nbr);
   }
}
