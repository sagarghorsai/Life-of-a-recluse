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
    public AudioClip background;

    [Header("---------- Audio Clips ----------")]
    public AudioClipInfo[] audioList; // Using the custom class

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(string audioName)
    {
        foreach (AudioClipInfo audioInfo in audioList)
        {
            if (audioInfo.name == audioName)
            {
                SFXSource.PlayOneShot(audioInfo.clip);
                Debug.Log($"Player{audioInfo.name}");
                return; // Exit once the clip is found and played
            }
        }

        Debug.LogWarning($"Audio clip with name '{audioName}' not found.");
    }
}




[System.Serializable]
public class AudioClipInfo
{
    public string name; // Word associated with the audio clip
    public AudioClip clip; // The audio clip
}
