using TMPro;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    public static DayCounter Instance { get; private set; }
    public int dayCount = 1;
    public int HighCount = 1;
    private void Awake()
    {
        // Ensure that only one instance of DayCounter exists and persists between scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load
            Instance = this;

            HighCount = PlayerPrefs.GetInt("HighCount", 1);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates if they exist
        }

    }


    public void Next()
    {
        if (dayCount > HighCount)
        {
            HighCount = dayCount;
            PlayerPrefs.SetInt("HighCount", HighCount);
            Debug.Log(HighCount);
        }

    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("HighCount", HighCount);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}