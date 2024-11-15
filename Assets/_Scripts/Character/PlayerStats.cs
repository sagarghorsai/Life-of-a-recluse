using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("---------- Player Stats ----------")]
    public float playerSprintSpeed;
    public float playerStamina;
    public float playerStaminaRegenRate;
    public float panicMax;

    // Default values for initialization
    private const float DEFAULT_SPRINT_SPEED = 5f;
    private const float DEFAULT_STAMINA = 1f;
    private const float DEFAULT_STAMINA_REGEN = 0.2f;
    private const float DEFAULT_PANICMAX = 10f;

    // PlayerPrefs keys
    private const string SPRINT_SPEED_KEY = "PlayerSprintSpeed";
    private const string STAMINA_KEY = "PlayerStamina";
    private const string STAMINA_REGEN_KEY = "PlayerStaminaRegen";
    private const string PANICMAX_KEY = "PanicMax";

    private PlayerMovement playerMovement;
    private EXPManager expManager;
    private PlayerStatsUI playerStatsUI;
    private EXPManager xpManager;
    private PanicMeter PanicMeter;

    void Awake()
    {
        LoadStats();
    }

    void Start()
    {
        expManager = FindObjectOfType<EXPManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerStatsUI = FindObjectOfType<PlayerStatsUI>();
        PanicMeter = FindObjectOfType<PanicMeter>();

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
            playerStamina = playerMovement.maxStamina;
            playerStaminaRegenRate = playerMovement.staminaRegenRate;
            panicMax = PanicMeter.panicMax;
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
        playerStamina = DEFAULT_STAMINA;
        playerStaminaRegenRate = DEFAULT_STAMINA_REGEN;
        panicMax = DEFAULT_PANICMAX;
        SaveStats();
    }
   
    public void LoadStats()
    {
        playerSprintSpeed = PlayerPrefs.GetFloat(SPRINT_SPEED_KEY, DEFAULT_SPRINT_SPEED);
        playerStamina = PlayerPrefs.GetFloat(STAMINA_KEY, DEFAULT_STAMINA);
        playerStaminaRegenRate = PlayerPrefs.GetFloat(STAMINA_REGEN_KEY, DEFAULT_STAMINA_REGEN);
        panicMax = PlayerPrefs.GetFloat(PANICMAX_KEY, DEFAULT_PANICMAX);
    }

    public void SaveStats()
    {
        PlayerPrefs.SetFloat(SPRINT_SPEED_KEY, playerSprintSpeed);
        PlayerPrefs.SetFloat(STAMINA_KEY, playerStamina);
        PlayerPrefs.SetFloat(STAMINA_REGEN_KEY, playerStaminaRegenRate);
        PlayerPrefs.SetFloat(PANICMAX_KEY, panicMax);
        PlayerPrefs.Save();
    }

    private void UpdatePlayerMovement()
    {
        if (playerMovement != null)
        {
            playerMovement.UpdateStatsFromPlayerStats();
        }
    }

    public void IncreaseMaxStamina()
    {
        if (expManager.UseUpgradePoint() &&playerStamina <5f)
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
        if (expManager.UseUpgradePoint()&&playerSprintSpeed<10)
        {
            playerSprintSpeed += 0.2f;
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
        if (expManager.UseUpgradePoint()&&playerStaminaRegenRate < 2f)
        {
            playerStaminaRegenRate += 0.05f;
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

    public void IncreasePanicMax()
    {
        if (expManager.UseUpgradePoint() && panicMax <25)
        {
            panicMax += 1f;
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