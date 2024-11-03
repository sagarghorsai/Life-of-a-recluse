using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public TextMeshProUGUI tutorialText; // Reference to the UI text element
    public GameObject tasklist; // Reference to the grocery list UI
    private string[] messages = {
        "Press W, A, S, or D to move around",
        "Holding Shift lets you sprint, but your sprint meter goes down.\n [F] To Continue",
        "By pressing the ESC key, You can open the Pause Menu.\n [F] To Continue",
        "You can Open Grocery List by pressing Tab. \n [F] To Continue",
        "Look at the name and picture in the grocery list. \n [F] To Continue",
        "Press E to pick up the item.\n [F] To Continue",
        "Watch out for random people! If you get too close, your panic meter will fill up.\n [F] To Continue",
        "Once your panic meter fills up, You Lose \n [F] To Continue",
        "Watch the clock! If it hits 0, the manager will kick you out.\n [F] To Continue",
        "Once you've collected everything on your grocery list, go to check out.\n [F] To Continue"
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