using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckOut : MonoBehaviour
{
    public DayCounter dayCounter;
    public TextMeshProUGUI dayText;
    private TaskList tasklist;
    private void Start()
    {
        // Find and reference the DayCounter script in the scene
        dayCounter = FindObjectOfType<DayCounter>();
        tasklist = FindObjectOfType<TaskList>();

        // Log an error if DayCounter is not found
        if (dayCounter == null)
        {
            Debug.LogError("DayCounter not found in the scene! Make sure you have a DayCounter component.");
        }
        dayText.text = $"Day \n{dayCounter.dayCount}";
        
    }
  
    public void Checkout()
    {
        if (tasklist.canCheckout)
        {
            // Increment the day counter
            if (dayCounter != null)
            {
                dayCounter.dayCount += 1;
                Debug.Log("Player checked out! Day count increased to: " + dayCounter.dayCount);
            }

            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");

        }
        else
        {
            Debug.Log("Checkout failed. All tasks must be completed before you can check out.");
        }
    }
}
