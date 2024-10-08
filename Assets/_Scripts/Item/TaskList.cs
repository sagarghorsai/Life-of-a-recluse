using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // For scene loading

public class TaskList : MonoBehaviour
{
    public List<GroceryTile> allGroceryTiles; // All available grocery tiles
    public List<GroceryTile> items; // Selected items for the task list
    public GameObject buttonPrefab;
    public int taskNum = 1;
    public RectTransform scrollViewContent;
    private Dictionary<string, TextMeshProUGUI> itemButtons = new Dictionary<string, TextMeshProUGUI>();
    private int completedTasks = 0;
    public int numberOfTasks = 5; // Number of tasks to select randomly

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

            // Add listener for manual clicking if needed
            newButton.GetComponent<Button>().onClick.AddListener(() => OnButtonClick(buttonText));
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
                buttonText.text = $"<s>{buttonText.text}</s>";
                completedTasks++;

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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Win");
        Debug.Log("All tasks completed! Loading win scene...");
    }

    void OnButtonClick(TextMeshProUGUI buttonText)
    {
        if (!buttonText.text.Contains("<s>"))
        {
            buttonText.text = $"<s>{buttonText.text}</s>";
            completedTasks++;

            // Check if all tasks are completed
            if (completedTasks >= items.Count)
            {
                OnAllTasksCompleted();
            }
        }
    }
}
