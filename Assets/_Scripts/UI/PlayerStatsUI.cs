using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : MonoBehaviour
{
    public PlayerStats playerStats;
    public EXPManager expManager;

    // UI Text Elements to display current stat values
    public TextMeshProUGUI sprintSpeedText;
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI staminaRegenText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI upgradePoint;

    // UI Buttons to upgrade stats
    public Button increaseSprintSpeedButton;
    public Button increaseStaminaButton;
    public Button increaseStaminaRegenButton;

    void OnEnable()
    {
        UpdateUI();
        increaseSprintSpeedButton.onClick.AddListener(() => playerStats.IncreaseSprintSpeed());
        increaseStaminaButton.onClick.AddListener(() => playerStats.IncreaseStamina());
        increaseStaminaRegenButton.onClick.AddListener(() => playerStats.IncreaseStaminaRegen());
    }


    public void UpdateUI()
    {
        // Update the UI text elements with current stat values
        sprintSpeedText.text = playerStats.playerSprintSpeed.ToString("F1");
        staminaText.text =  playerStats.playerStamina.ToString("F1");
        staminaRegenText.text =playerStats.playerStaminaRegenRate.ToString("F1");
        levelText.text = expManager.currentLevel.ToString();
        upgradePoint.text = expManager.upgradePoints.ToString();
    }

   
}
