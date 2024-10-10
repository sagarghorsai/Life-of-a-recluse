using TMPro;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    public static DayCounter Instance { get; private set; }
    public int dayCount = 1;

    private void Awake()
    {
        // Ensure that only one instance of DayCounter exists and persists between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates if they exist
        }
        
    }
}
