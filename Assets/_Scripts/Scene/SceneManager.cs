using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneManager : MonoBehaviour
{
    DayCounter dayCounter;
    LevelRandomizer randomizer;

    private void Start()
    {
        dayCounter = FindObjectOfType<DayCounter>();

        randomizer = FindAnyObjectByType<LevelRandomizer>();
        if (dayCounter == null)
        {
            Debug.LogError("DayCounter not found in the scene! Make sure you have a DayCounter component.");
        }
    }
   

    public void StartGame()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("L1");
        randomizer.RandomizedLevel();
        AudioManager.Instance.PlayMusic("LevelMusic");
        dayCounter.dayCount = 1;
        Debug.Log("New Game Clicked");
    }
    public void Tutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
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

    public void NextDay()
    {
        randomizer.RandomizedLevel();
        AudioManager.Instance.PlayMusic("LevelMusic");
    }
}
