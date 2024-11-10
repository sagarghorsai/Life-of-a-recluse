using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EXPManager : MonoBehaviour
{
    [Header("Experience")]
    [SerializeField] AnimationCurve experienceCurve;
    public int currentLevel, totalExperience, upgradePoints;
    int previousLevelsExperience, nextLevelsExperience;

    [Header("Interface")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI experienceText;
    [SerializeField] Image experienceFill;

    // PlayerPrefs keys
    private const string LEVEL_KEY = "PlayerLevel";
    private const string TOTAL_EXP_KEY = "TotalExperience";
    private const string UPGRADE_POINTS_KEY = "UpgradePoints";

    // Default values
    private const int DEFAULT_LEVEL = 1;
    private const int DEFAULT_EXP = 0;
    private const int DEFAULT_UPGRADE_POINTS = 0;

    void Awake()
    {
        LoadProgress();
    }

    void Start()
    {
        UpdateLevel();
    }

    public void LoadProgress()
    {
        currentLevel = PlayerPrefs.GetInt(LEVEL_KEY, DEFAULT_LEVEL);
        totalExperience = PlayerPrefs.GetInt(TOTAL_EXP_KEY, DEFAULT_EXP);
        upgradePoints = PlayerPrefs.GetInt(UPGRADE_POINTS_KEY, DEFAULT_UPGRADE_POINTS);
    }

    public void SaveProgress()
    {
        PlayerPrefs.SetInt(LEVEL_KEY, currentLevel);
        PlayerPrefs.SetInt(TOTAL_EXP_KEY, totalExperience);
        PlayerPrefs.SetInt(UPGRADE_POINTS_KEY, upgradePoints);
        PlayerPrefs.Save();
    }

    public void AddExperience(int amount)
    {
        totalExperience += amount;
        CheckForLevelUp();
        UpdateInterface();
        SaveProgress();
    }

    void CheckForLevelUp()
    {
        if (totalExperience >= nextLevelsExperience)
        {
            currentLevel++;
            upgradePoints++;  // Add upgrade points for leveling up
            UpdateLevel();
            SaveProgress();
            // Start level up sequence (SFX)
        }
    }

    void UpdateLevel()
    {
        previousLevelsExperience = (int)experienceCurve.Evaluate(currentLevel);
        nextLevelsExperience = (int)experienceCurve.Evaluate(currentLevel + 1);
        UpdateInterface();
    }

    void UpdateInterface()
    {
        int start = totalExperience - previousLevelsExperience;
        int end = nextLevelsExperience - previousLevelsExperience;

        if (levelText != null)
            levelText.text = $"{currentLevel}";

        if (experienceText != null)
            experienceText.text = $"{start} EXP/{end} EXP";

        if (experienceFill != null)
            experienceFill.fillAmount = (float)start / end;
    }

    public bool UseUpgradePoint()
    {
        if (upgradePoints > 0)
        {
            upgradePoints--;
            SaveProgress();
            return true;
        }
        return false;
    }

    // Optional: Add method to reset progress
    public void ResetProgress()
    {
        currentLevel = DEFAULT_LEVEL;
        totalExperience = DEFAULT_EXP;
        upgradePoints = DEFAULT_UPGRADE_POINTS;
        SaveProgress();
        UpdateLevel();
        Debug.Log("Progress reset to default values");
    }

    // Optional: Save when the game is quitting
    private void OnApplicationQuit()
    {
        SaveProgress();
    }

    // Optional: Save when the game is paused/backgrounded
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            SaveProgress();
        }
    }
}