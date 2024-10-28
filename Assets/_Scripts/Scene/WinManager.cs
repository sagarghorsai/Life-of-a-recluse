using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public TextMeshProUGUI DayText;


    DayCounter dayCounter;
    private void Start()
    {
        dayCounter = FindObjectOfType<DayCounter>();

        if (dayCounter == null)
        {
            Debug.LogError("DayCounter not found in the scene! Make sure you have a DayCounter component.");
        }
    }
    private void Update()
    {
        DayText.text = $"Congrats you've completed day  \n{dayCounter.dayCount-1}"; 
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        AudioManager.Instance.PlayMusic("LevelMusic");
    }
}
