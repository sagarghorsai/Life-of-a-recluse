using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // Static variable to track game state
    public GameObject pauseMenuUI;           // Reference to the pause menu UI

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Detect "Escape" key press
        {
            Debug.Log("Pressed ESC");
            // Toggle pause/resume based on the current state
            if (GameIsPaused)
            {
                Resume();  // If the game is already paused, resume it
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

    public void Quit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void Menu()
    {
        Debug.Log("Load Menu");
        Time.timeScale = 1f;           
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");


    }

    public void Options()
    {
        Debug.Log("Load Options");
        // Add options menu loading logic here
    }
}
