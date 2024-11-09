using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("---------- Player Stats ----------")]
    public float playerSprintSpeed;
    public float playerWalkSpeed;
    public float playerStamina;
    public float playerStaminaRegenRate;

    PlayerMovement playerMovement;




    EXPManager expManager;

    void Start()
    {
        expManager = FindObjectOfType<EXPManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerSprintSpeed = playerMovement.sprintSpeed;
        playerWalkSpeed = playerMovement.walkSpeed;
        playerStamina = playerMovement.maxStamina;
        playerStaminaRegenRate = playerMovement.staminaRegenRate;
    }



    public void IncreaseStamina()
    {
        if (expManager.UseUpgradePoint())
        {
            playerStamina += 0.2f;
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
        }
        else
        {
            Debug.Log("No Upgrade Point");
        }
    }
}
