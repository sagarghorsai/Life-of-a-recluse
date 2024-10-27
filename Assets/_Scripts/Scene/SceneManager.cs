using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    DayCounter dayCounter;
    private void Start()
    {
        dayCounter = FindObjectOfType<DayCounter>();

        if (dayCounter == null)
        {
            Debug.LogError("DayCounter not found in the scene! Make sure you have a DayCounter component.");
        }
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        AudioManager.Instance.PlayMusic("LevelMusic");
        dayCounter.dayCount = 1;
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
