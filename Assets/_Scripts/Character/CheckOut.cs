using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckOut : MonoBehaviour
{
    private DayCounter dayCounter;
    public TextMeshProUGUI dayText;
    private AudioManager audioManager;
    EXPManager expManager;
    PlayerStats playerStats;

    private TaskList tasklist;
    private void Start()
    {
        // Find and reference the DayCounter script in the scene
        dayCounter = FindObjectOfType<DayCounter>();
        tasklist = FindObjectOfType<TaskList>();

        expManager = FindObjectOfType<EXPManager>();
        playerStats = FindObjectOfType<PlayerStats>();

        // Log an error if DayCounter is not found
        if (dayCounter == null)
        {
            Debug.LogError("DayCounter not found in the scene! Make sure you have a DayCounter component.");
        }
        dayText.text = $"Day \n{dayCounter.dayCount}";
        Debug.Log($"HighScore is {dayCounter.HighCount}");
        if (audioManager != null)
        {
            audioManager = FindObjectOfType<AudioManager>(); // Find the AudioManager in the scene
        }


    }

    public void Checkout()
    {

        if (tasklist.canCheckout)
        {
           if (UnityEngine.SceneManagement.SceneManager.sceneCount ==0)
            {
                expManager.ResetProgress();
                playerStats.ResetStats();
                UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
                AudioManager.Instance.PlayMusic("MenuMusic");
            }


            Debug.Log($"{dayCounter}+1");
            // Increment the day counter
            if (dayCounter != null)
            {
                DayCounter.Instance.Next();
                dayCounter.dayCount += 1;
                AudioManager.Instance.PlaySFX("CheckOut");
                Debug.Log("Player checked out! Day count increased to: " + dayCounter.dayCount);

            }
            tasklist.canCheckout = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Win");

        }
        else
        {
            Debug.Log("Checkout failed. All tasks must be completed before you can check out.");
        }
    }
}
