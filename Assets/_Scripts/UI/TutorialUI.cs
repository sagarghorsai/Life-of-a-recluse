using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // Reference to the UI text element
    private string[] messages = {
    "Use WASD to move around the store.\n[E] to continue",
    "You can hold Shift to sprint. Once you've used all of your stamina bar, you must wait for it to regenerate.\n[E] to continue",
    "Press TAB to open/close your Grocery List.\n[E] to continue",
    "Check the name and image in the Grocery List to see what you need to pick up.\n[E] to continue",
    "To finish your day, you must collect all items on your Grocery List.\n[E] to continue",
    "When you pick up items, you gain XP, which helps you level up.\n[E] to continue",
    "Each time you level up, you earn an upgrade point to improve your stats.\n[E] to continue",
    "Watch the timer! If it hits 0, the manager will come after you.\n[E] to continue",
    "There are other customers in the store. Your goal is to avoid them.\n[E] to continue",
    "If you get too close to other customers, your Panic Meter will increase.\n[E] to continue",
    "Be careful with your Panic Meter. If it fills up completely, you lose.\n[E] to continue",
    "The Panic Meter will decrease after staying away from the enemies' interaction zone.\n[E] to continue",
    "Press ESC to open the pause menu.\n[E] to continue",
    "In the pause menu, you can adjust sound settings or use upgrade points to improve your stats.\n[E] to continue",
    "Press Resume to return to the game.\n[E] to continue",
    "Good luck, Player!\n[E] to close!"
};


    private int currentStep = 0;
    private bool hasMoved = false;
    public GameObject textBackground;

    void Start()
    {
        ShowMessage(); // Display the first message
    }

    void Update()
    {
        if (currentStep == 0)
        {
            // Check for movement in the first step
            CheckMovement();
            if (hasMoved)
            {
                AdvanceStep();
            }
        }
       
        else
        {
            // For all other steps, wait for the player to press 'E' to continue
            if (Input.GetKeyDown(KeyCode.E))
            {
                AdvanceStep();
            }
        }
    }

    void ShowMessage()
    {
        if (currentStep < messages.Length)
        {
            tutorialText.text = messages[currentStep];
        }
        else
        {
            textBackground.SetActive(false);
        }
    }

    void AdvanceStep()
    {
        currentStep++;
        ShowMessage();
    }

    void CheckMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            hasMoved = true;
        }
    }
}