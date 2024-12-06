using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Background Music ----------")]
    public AudioClipInfo[] backgroundList;

    [Header("---------- Audio Clips ----------")]
    public AudioClipInfo[] audioList; // Using the custom class

    private void Start()
    {
        musicSource.clip = backgroundList[0].clip;
        musicSource.Play();
    }


    public void PlayMusic(string audioName)
    {
        foreach (AudioClipInfo audioInfo in backgroundList)
        {
            if (audioInfo.name == audioName)
            {
                musicSource.clip = audioInfo.clip;
                musicSource.Play();

                Debug.Log($"Player{audioInfo.name}");
                return; // Exit once the clip is found and played
            }
        }

        Debug.LogWarning($"Audio clip with name '{audioName}' not found.");
    }
    public void PlaySFX(string audioName)
    {
        foreach (AudioClipInfo audioInfo in audioList)
        {
            if (audioInfo.name == audioName)
            {
                SFXSource.PlayOneShot(audioInfo.clip);
                return; // Exit once the clip is found and played
            }
        }

    }

    private string currentLoopingSFX; // To track the currently looping SFX

    public void PlaySFXLoop(string audioName)
    {
        // Avoid restarting the same loop
        if (currentLoopingSFX == audioName && SFXSource.isPlaying && SFXSource.loop)
        {
            return;
        }

        foreach (AudioClipInfo audioInfo in audioList)
        {
            if (audioInfo.name == audioName)
            {
                SFXSource.clip = audioInfo.clip; // Assign the clip
                SFXSource.loop = true;          // Enable looping
                SFXSource.Play();               // Start playing
                currentLoopingSFX = audioName;  // Update current looping SFX
                return;                         // Exit after playing the desired sound
            }
        }

        Debug.LogWarning($"Audio clip with name {audioName} not found in the audio list.");
    }

    public void StopSFXLoop()
    {
        if (SFXSource != null && SFXSource.isPlaying)
        {
            SFXSource.Stop();
            SFXSource.loop = false; // Disable looping
            currentLoopingSFX = null; // Clear the tracking variable
        }
    }


}




[System.Serializable]
public class AudioClipInfo
{
    public string name; // Word associated with the audio clip
    public AudioClip clip; // The audio clip
}
