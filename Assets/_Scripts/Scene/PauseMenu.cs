using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // Static variable to track game state
    public GameObject pauseMenuUI;           // Reference to the pause menu UI
    public GameObject resumeButton;
    public GameObject menuButton;
    public GameObject optionButton;
    public GameObject quitButton;
    public GameObject returnButton;
    public GameObject settingsMenu;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Detect "Escape" key press
        {
            Debug.Log("Pressed ESC");
            // Toggle pause/resume based on the current state
            if (GameIsPaused)
            {
                Resume();  // If the game is already paused, resume it
                Return();  // Unpauses the game but resets 'Pause Menus' back to normal status

            }
            else
            {
                Pause();   // If the game is not paused, pause it
            }
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

    // Placeholder for quit function
    public void Quit()
    {
        Debug.Log("Quit Game");
        // Add quit logic here, e.g., Application.Quit();
    }

    // Placeholder for loading the main menu
    public void Menu()
    {
        Debug.Log("Load Menu");
        // Add menu loading logic here
    }

    // Placeholder for loading options
    public void Options()
    {
        resumeButton.SetActive(false);
        menuButton.SetActive(false);
        optionButton.SetActive(false);
        quitButton.SetActive(false);
        returnButton.SetActive(true);
        settingsMenu.SetActive(true);
        Debug.Log("Load Options");
        // Add options menu loading logic here
    }

    public void Return()
    {
        resumeButton.SetActive(true);
        menuButton.SetActive(true);
        optionButton.SetActive(true);
        quitButton.SetActive(true);
        returnButton.SetActive(false);
        settingsMenu.SetActive(false);
        Debug.Log("Load Return");
    }
}
