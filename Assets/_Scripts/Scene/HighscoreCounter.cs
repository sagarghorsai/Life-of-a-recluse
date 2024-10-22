using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreCounter : MonoBehaviour
{
    public static HighscoreCounter Instance { get; private set; }
    public int highscoreCount;
    
    private void Awake()
    {
        // Ensure that only one instance of the HighscoreCounter exists and doesn't get destroyed between scenes.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load.
        }
        else
        {
            Destroy(gameObject); // Destroys any duplicates if they exist.
        }
    }
}
