using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    [Header("Player Stats")]
    public PlayerStats playerStats;
    public EXPManager expManager;

    [Header("UI Text Elements")]
    public TextMeshProUGUI sprintSpeedText;
    public TextMeshProUGUI maxStaminaText;
    public TextMeshProUGUI staminaRegenText;
    public TextMeshProUGUI maxPanicsText;
    public TextMeshProUGUI upgradePointsText;
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI experienceText;

    [Header("UI Fill Elements")]
    public Image experienceFill;

    [Header("Upgrade Buttons")]
    public Button increaseSprintSpeedButton;
    public Button increaseMaxStaminaButton;
    public Button increaseStaminaRegenButton;
    public Button increaseMaxPanicButton;

    void OnEnable()
    {
        UpdateUI();
        increaseSprintSpeedButton.onClick.AddListener(() => playerStats.IncreaseSprintSpeed());
        increaseMaxStaminaButton.onClick.AddListener(() => playerStats.IncreaseMaxStamina());
        increaseStaminaRegenButton.onClick.AddListener(() => playerStats.IncreaseStaminaRegen());
        increaseMaxPanicButton.onClick.AddListener(() => playerStats.IncreasePanicMax());
    }

    public void UpdateUI()
    {
        // Update the UI text elements with current stat values
        sprintSpeedText.text = playerStats.playerSprintSpeed.ToString("F1");
        maxStaminaText.text = playerStats.playerStamina.ToString("F1");
        staminaRegenText.text = playerStats.playerStaminaRegenRate.ToString("F1");
        maxPanicsText.text = playerStats.panicMax.ToString("F1");
        upgradePointsText.text = "Upgrade points: " + expManager.upgradePoints.ToString();
        currentLevelText.text = expManager.currentLevel.ToString();

        // Update experience display and fill
        float currentExp = expManager.totalExperience - expManager.previousLevelsExperience;
        float nextLevelExp = expManager.nextLevelsExperience - expManager.previousLevelsExperience;
        experienceText.text = $"{currentExp:F0} / {nextLevelExp:F0} EXP";
        experienceFill.fillAmount = currentExp / nextLevelExp;
    }
}
