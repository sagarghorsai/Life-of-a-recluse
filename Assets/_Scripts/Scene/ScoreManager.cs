using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text hiScoreText;
    public static int scoreCount;
    public static int highscoreCounter;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HighestDay"))
        {
            highscoreCounter = PlayerPrefs.GetInt("HighestDay");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreCount > highscoreCounter)
        {
            highscoreCounter = scoreCount;
            PlayerPrefs.SetInt("HighestDay", highscoreCounter);
        }

        scoreText.text = "Score: " + scoreCount;
        hiScoreText.text = "Highest Day: " + highscoreCounter;
    }
}
