using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
  


    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        AudioManager.Instance.PlayMusic("LevelMusic");
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        AudioManager.Instance.PlayMusic("MenuMusic");
    }

    public void Quit()
    {
        Application.Quit();

    }
}
