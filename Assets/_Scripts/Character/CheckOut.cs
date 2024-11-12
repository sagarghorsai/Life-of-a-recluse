using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckOut : MonoBehaviour
{
    private DayCounter dayCounter;
    public TextMeshProUGUI dayText;
    private AudioManager audioManager;
    private EXPManager expManager;
    private PlayerStats playerStats;
    private TaskList tasklist;
    private DifficultyManager difficultyManager;



    private void Start()
    {
        dayCounter = FindObjectOfType<DayCounter>();
        tasklist = FindObjectOfType<TaskList>();
        expManager = FindObjectOfType<EXPManager>();
        playerStats = FindObjectOfType<PlayerStats>();
        difficultyManager = FindAnyObjectByType<DifficultyManager>();

        if (dayCounter == null)
        {
            Debug.LogError("DayCounter not found in the scene! Make sure you have a DayCounter component.");
        }
        dayText.text = $"Day \n{dayCounter.dayCount}";
        Debug.Log($"HighScore is {dayCounter.HighCount}");

        if (audioManager != null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }
    }

    public void Checkout()
    {
        if (tasklist.canCheckout)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Tutorial")
            {
                expManager.ResetProgress();
                playerStats.ResetStats();
                UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
                AudioManager.Instance.PlayMusic("MenuMusic");
            }
            else
            {
                if (dayCounter != null)
                {
                    DayCounter.Instance.Next();
                    dayCounter.dayCount += 1;
                    AudioManager.Instance.PlaySFX("CheckOut");
                    Debug.Log("Player checked out! Day count increased to: " + dayCounter.dayCount);
                }

                // Increase the difficulty in the ProgressionMechanic script
                if (difficultyManager != null)
                {
                    difficultyManager.IncreaseDifficulty();
                }

                tasklist.canCheckout = false;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
            }
        }
        else
        {
            Debug.Log("Checkout failed. All tasks must be completed before you can check out.");
        }
    }
}