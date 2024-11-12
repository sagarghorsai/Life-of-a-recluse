using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("---------- Player Stats ----------")]
    public float playerSprintSpeed;
    public float playerWalkSpeed;
    public float playerStamina;
    public float playerStaminaRegenRate;

    // Default values for initialization
    private const float DEFAULT_SPRINT_SPEED = 5f;
    private const float DEFAULT_WALK_SPEED = 3f;
    private const float DEFAULT_STAMINA = 1f;
    private const float DEFAULT_STAMINA_REGEN = 0.2f;

    // PlayerPrefs keys
    private const string SPRINT_SPEED_KEY = "PlayerSprintSpeed";
    private const string WALK_SPEED_KEY = "PlayerWalkSpeed";
    private const string STAMINA_KEY = "PlayerStamina";
    private const string STAMINA_REGEN_KEY = "PlayerStaminaRegen";

    private PlayerMovement playerMovement;
    private EXPManager expManager;
    private PlayerStatsUI playerStatsUI;
    private EXPManager xpManager;

    void Awake()
    {
        LoadStats();
    }

    void Start()
    {
        expManager = FindObjectOfType<EXPManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerStatsUI = FindObjectOfType<PlayerStatsUI>();

        // Only initialize from PlayerMovement if no saved data exists
        if (!PlayerPrefs.HasKey(SPRINT_SPEED_KEY))
        {
            InitializeFromPlayerMovement();
        }

        // Update PlayerMovement with our stats
        UpdatePlayerMovement();

        // Update UI
        if (playerStatsUI != null)
        {
            playerStatsUI.UpdateUI();
        }
    }

    private void InitializeFromPlayerMovement()
    {
        if (playerMovement != null)
        {
            playerSprintSpeed = playerMovement.sprintSpeed;
            playerWalkSpeed = playerMovement.walkSpeed;
            playerStamina = playerMovement.maxStamina;
            playerStaminaRegenRate = playerMovement.staminaRegenRate;
            SaveStats();
        }
        else
        {
            SetDefaultStats();
        }
    }

    private void SetDefaultStats()
    {
        playerSprintSpeed = DEFAULT_SPRINT_SPEED;
        playerWalkSpeed = DEFAULT_WALK_SPEED;
        playerStamina = DEFAULT_STAMINA;
        playerStaminaRegenRate = DEFAULT_STAMINA_REGEN;
        SaveStats();
    }
   
    public void LoadStats()
    {
        playerSprintSpeed = PlayerPrefs.GetFloat(SPRINT_SPEED_KEY, DEFAULT_SPRINT_SPEED);
        playerWalkSpeed = PlayerPrefs.GetFloat(WALK_SPEED_KEY, DEFAULT_WALK_SPEED);
        playerStamina = PlayerPrefs.GetFloat(STAMINA_KEY, DEFAULT_STAMINA);
        playerStaminaRegenRate = PlayerPrefs.GetFloat(STAMINA_REGEN_KEY, DEFAULT_STAMINA_REGEN);
    }

    public void SaveStats()
    {
        PlayerPrefs.SetFloat(SPRINT_SPEED_KEY, playerSprintSpeed);
        PlayerPrefs.SetFloat(WALK_SPEED_KEY, playerWalkSpeed);
        PlayerPrefs.SetFloat(STAMINA_KEY, playerStamina);
        PlayerPrefs.SetFloat(STAMINA_REGEN_KEY, playerStaminaRegenRate);
        PlayerPrefs.Save();
    }

    private void UpdatePlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.UpdateStatsFromPlayerStats();
        }
    }

    public void IncreaseStamina()
    {
        if (expManager.UseUpgradePoint())
        {
            playerStamina += 0.2f;
            UpdatePlayerMovement();
            SaveStats();
            playerStatsUI.UpdateUI();
            Debug.Log($"Stamina increased to: {playerStamina}");
        }
        else
        {
            Debug.Log("No Upgrade Point");
        }
    }

    public void IncreaseSprintSpeed()
    {
        if (expManager.UseUpgradePoint())
        {
            playerSprintSpeed += 0.5f;
            UpdatePlayerMovement();
            SaveStats();
            playerStatsUI.UpdateUI();
            Debug.Log($"Sprint Speed increased to: {playerSprintSpeed}");
        }
        else
        {
            Debug.Log("No Upgrade Point");
        }
    }

    public void IncreaseStaminaRegen()
    {
        if (expManager.UseUpgradePoint())
        {
            playerStaminaRegenRate += 0.1f;
            UpdatePlayerMovement();
            SaveStats();
            playerStatsUI.UpdateUI();
            Debug.Log($"Stamina Regen increased to: {playerStaminaRegenRate}");
        }
        else
        {
            Debug.Log("No Upgrade Point");
        }
    }

    public void ResetStats()
    {
        SetDefaultStats();
        UpdatePlayerMovement();
        playerStatsUI.UpdateUI();
        Debug.Log("Stats reset to default values");
    }

    private void OnApplicationQuit()
    {
        SaveStats();
    }
}