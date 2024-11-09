using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // Reference to the UI text element
    public GameObject tasklist; // Reference to the grocery list UI
    private string[] messages = {
    "Use W, A, S, D to move around.",
    "Hold Shift to sprint, but watch your sprint meter!\n [F] To Continue",
    "Press ESC to open the Pause Menu. Here you can resume, exit to the menu, or check stats.\nPress ESC again to unpause.\n [F] To Continue",
    "Press Tab to open your Grocery List.\n [F] To Continue",
    "Check the name and picture in your Grocery List to know what items to collect.\n [F] To Continue",
    "Press E to pick up an item—but only if it’s on your list.\n [F] To Continue",
    "Be careful around other shoppers! Getting too close will raise your panic meter.\n [F] To Continue",
    "If your panic meter fills completely, you’ll lose!\n [F] To Continue",
    "Watch the clock! If it hits 0, the manager will come kick you out.\n [F] To Continue",
    "Once you've collected all items, head to checkout to finish.\n [F] To Continue",
    "Collect groceries to earn EXP. Level up to unlock upgrade points and improve your stats through the Stats option in the Pause Menu.\n [F] To Continue"
};


    private int currentStep = 0;
    private bool hasMoved = false;

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
            if (Input.GetKeyDown(KeyCode.F))
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
            tutorialText.text = ""; // Clear text when the tutorial is done
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