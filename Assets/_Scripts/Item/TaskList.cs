using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class TaskList : MonoBehaviour
{
    [Header("---------- Item List ----------")]
    public List<GroceryTile> allGroceryTiles; // All available grocery tiles
    public List<GroceryTile> items; // Selected items for the task list

    [Header("---------- References ----------")]
    public GameObject buttonPrefab;
    public RectTransform scrollViewContent;
    public TextMeshProUGUI checkoutMessageText; // UI Text to show the message
    public Dictionary<string, List<TextMeshProUGUI>> itemButtons = new Dictionary<string, List<TextMeshProUGUI>>();

    [Header("---------- Task List ----------")]
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
            buttonText.text = items[i].groceryName;

            // Store button references
            if (!itemButtons.ContainsKey(items[i].groceryName))
            {
                itemButtons[items[i].groceryName] = new List<TextMeshProUGUI>();
            }
            itemButtons[items[i].groceryName].Add(buttonText);
        }
    }

    private void SelectRandomItems()
    {

        if (numberOfTasks > 5)
        {


            // Create a list of items grouped by type
            List<GroceryTile> groceryItems = allGroceryTiles.Where(item => item.tileType == TileType.Grocery).ToList();
            List<GroceryTile> flowerItems = allGroceryTiles.Where(item => item.tileType == TileType.Flower).ToList();
            List<GroceryTile> bakeryItems = allGroceryTiles.Where(item => item.tileType == TileType.Bakery).ToList();
            List<GroceryTile> meatItems = allGroceryTiles.Where(item => item.tileType == TileType.Meat).ToList();
            List<GroceryTile> shelvingItem = allGroceryTiles.Where(item => item.tileType == TileType.Shelving).ToList();

            // Ensure at least one item from each type is selected
            if (groceryItems.Count > 0) items.Add(groceryItems[Random.Range(0, groceryItems.Count)]);
            if (flowerItems.Count > 0) items.Add(flowerItems[Random.Range(0, flowerItems.Count)]);
            if (bakeryItems.Count > 0) items.Add(bakeryItems[Random.Range(0, bakeryItems.Count)]);
            if (meatItems.Count > 0) items.Add(meatItems[Random.Range(0, meatItems.Count)]);
            if (shelvingItem.Count > 0) items.Add(shelvingItem[Random.Range(0, shelvingItem.Count)]);
        }
        // Fill the remaining slots randomly from the whole grocery tile list, excluding already added items
        HashSet<int> selectedIndices = new HashSet<int>();
        while (items.Count < numberOfTasks && selectedIndices.Count < allGroceryTiles.Count)
        {
            int randomIndex = Random.Range(0, allGroceryTiles.Count);

            // Ensure the item isn't already in the task list
            if (!items.Contains(allGroceryTiles[randomIndex]))
            {
                selectedIndices.Add(randomIndex);
                items.Add(allGroceryTiles[randomIndex]);
            }
        }
    }

    public void StrikeThroughItem(string itemName)
    {
        if (itemButtons.ContainsKey(itemName))
        {
            foreach (var buttonText in itemButtons[itemName])
            {
                if (!buttonText.text.Contains("<s>"))
                {
                    completedTasks++;

                    // Create a new strike line
                    GameObject strikeLine = Instantiate(strikeLinePrefab, buttonText.transform);
                    RectTransform lineTransform = strikeLine.GetComponent<RectTransform>();

                    // Match the width of the text
                    lineTransform.sizeDelta = new Vector2(buttonText.preferredWidth * 0.8f, StrikeHeight); // Set to 80% of the preferred width
                    lineTransform.anchoredPosition = new Vector2(0, 0); // Adjust position to match the center of the text

                    // Optionally, you can mark the text as struck through
                    buttonText.text = "<s>" + buttonText.text + "</s>"; // Mark as struck through
                }
            }

            // Check if all tasks are completed
            if (completedTasks >= items.Count)
            {
                OnAllTasksCompleted();
            }
        }
        else
        {
            Debug.Log("Not in the list");
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
