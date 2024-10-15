using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TaskList : MonoBehaviour
{
    [Header("---------- Item List ----------")]
    public List<GroceryTile> allGroceryTiles; // All available grocery tiles
    public List<GroceryTile> items; // Selected items for the task list

    [Header("---------- References ----------")]
    public GameObject buttonPrefab;
    public RectTransform scrollViewContent;
    public TextMeshProUGUI checkoutMessageText; // UI Text to show the message
    private Dictionary<string, TextMeshProUGUI> itemButtons = new Dictionary<string, TextMeshProUGUI>();

    [Header("---------- Task List ----------")]
    public int taskNum = 1;
    private int completedTasks = 0;
    public int numberOfTasks = 5; // Number of tasks to select randomly
    public bool canCheckout = false; // Flag to indicate if player can checkout

    public GameObject uiText; // Reference to your Text or TextMeshPro object
    public GameObject strikeLinePrefab; // Prefab for the line renderer (or use a UI Image)
    public int StrikeHeight;

    void Start()
    {
        // Randomly select items from allGroceryTiles
        SelectRandomItems();

        for (int i = 0; i < items.Count; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, scrollViewContent);
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            var buttonImage = newButton.transform.GetChild(1).GetComponent<Image>();

            // Set the sprite for the image
            buttonImage.sprite = items[i].sprite;

            // Set the text for the button
            buttonText.text = $"{taskNum} X " + items[i].groceryName;

            // Store button references
            itemButtons[items[i].groceryName] = buttonText;
        }


       
    }

    private void SelectRandomItems()
    {
        HashSet<int> selectedIndices = new HashSet<int>();
        while (selectedIndices.Count < numberOfTasks && selectedIndices.Count < allGroceryTiles.Count)
        {
            int randomIndex = Random.Range(0, allGroceryTiles.Count);
            selectedIndices.Add(randomIndex);
        }

        foreach (int index in selectedIndices)
        {
            items.Add(allGroceryTiles[index]);
        }
    }

    public void StrikeThroughItem(string itemName)
    {
        if (itemButtons.ContainsKey(itemName))
        {
            TextMeshProUGUI buttonText = itemButtons[itemName];
            if (!buttonText.text.Contains("<s>"))
            {
                completedTasks++;

                // Create a new strike line
                GameObject strikeLine = Instantiate(strikeLinePrefab, buttonText.transform);
                RectTransform lineTransform = strikeLine.GetComponent<RectTransform>();

                // Match the width of the text
                lineTransform.sizeDelta = new Vector2(buttonText.preferredWidth * 0.8f, StrikeHeight); // Set to 80% of the preferred width
                lineTransform.anchoredPosition = new Vector2(0, 0); // Adjust position to match the center of the text



                // Check if all tasks are completed
                if (completedTasks >= items.Count)
                {
                    OnAllTasksCompleted();
                }

            }
        }

      



    }

    private void OnAllTasksCompleted()
    {
        // Set canCheckout to true to enable the player to check out
        canCheckout = true;
        Debug.Log("All tasks completed! You can now check out at the cashier.");

        checkoutMessageText.text = "All tasks completed! Head to the cashier to check out.";
    }

    
}
