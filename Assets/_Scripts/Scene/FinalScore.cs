using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    public DayCounter dayCounter;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiScoreText;
    public TextMeshProUGUI finalText;
    // Start is called before the first frame update
    void Start()
    {
        dayCounter = FindObjectOfType<DayCounter>();

        if (dayCounter == null)
        {
            Debug.LogError("DayCounter not found in the scene! Make sure you have a DayCounter component.");
        }
        scoreText.text = $"You're moving onto Day: \n{dayCounter.dayCount}";
        hiScoreText.text = $"Most days survived in one run: \n{dayCounter.HighCount}";
        finalText.text = $"You failed on Day: \n{dayCounter.dayCount}";
    }

 
}
