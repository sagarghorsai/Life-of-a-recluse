using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // Static variable to track game state
    public GameObject pauseMenuUI;
    public GameObject optionMenuUI;
    public GameObject statsMenuUI;


    EXPManager expManager;
    PlayerStats playerStats;
    DifficultyManager difficultyManager;
    DayCounter dayCounter;



    private void Start()
    {
        expManager = FindAnyObjectByType<EXPManager>();
        playerStats = FindAnyObjectByType<PlayerStats>();
        difficultyManager = FindAnyObjectByType<DifficultyManager>();
        dayCounter = FindAnyObjectByType<DayCounter>(); 

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Detect "Escape" key press
        {
           
                Pause();   // If the game is not paused, pause it
            
        }
    }

    // Method to resume the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // Hide the pause menu UI
        Time.timeScale = 1f;           // Resume the game time
        GameIsPaused = false;          // Update the pause state
    }

    // Method to pause the game
    void Pause()
    {
        pauseMenuUI.SetActive(true);   // Show the pause menu UI
        Time.timeScale = 0f;           // Pause the game time
        GameIsPaused = true;           // Update the pause state
    }

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void Menu()
    {

        expManager.ResetProgress();
        playerStats.ResetStats();
        difficultyManager.DifficultyReset();
        dayCounter.dayCount = 1;

        Debug.Log("Load Menu");
        Time.timeScale = 1f;           
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        AudioManager.Instance.PlayMusic("MenuMusic");



    }

    public void Options()
    {
        pauseMenuUI.SetActive(false);   // Show the pause menu UI
        statsMenuUI.SetActive(false);
        optionMenuUI.SetActive(true);

        Debug.Log("Load Options");
    }

    public void Stats()
    {
        pauseMenuUI.SetActive(false);   // Show the pause menu UI
        statsMenuUI.SetActive(true);
        optionMenuUI.SetActive(false);
        Debug.Log("Load Stats");


    }

    public void Return()
    {
        pauseMenuUI.SetActive(true);   // Show the pause menu UI
        statsMenuUI.SetActive(false);
        optionMenuUI.SetActive(false);
        Debug.Log("Load Return");
    }

    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("HighCount");
        DayCounter.Instance.HighCount = 1; // Reset HighCount to default in the script
        PlayerPrefs.Save();
        Debug.Log("High score reset.");
    }
}
