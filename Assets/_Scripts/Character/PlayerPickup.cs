using UnityEngine.Tilemaps;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerPickup : MonoBehaviour
{

    public Tilemap groceryTilemap;  // Reference to the grocery Tilemap
    public TextMeshProUGUI pickUpText;  // Reference to UI Text for pickup
    private PlayerMovement playerMovement;  // Reference to the PlayerMovement script
    private GroceryTile currentGroceryTile;  // The tile in front of the player (if it's a grocery item)
    private Vector3Int frontTilePosition;  // Position of the tile in front of the player

    public TaskList taskList;  // Reference to the TaskList script
    private AudioManager audioManager; // Reference to the AudioManager script

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // Get PlayerMovement component
        pickUpText.gameObject.SetActive(false);  // Hide pickup text initially

        if (audioManager != null)
        {
            audioManager = FindObjectOfType<AudioManager>(); // Find the AudioManager in the scene
        }

    }

    void Update()
    {
        CheckForPickup();

        // If the player presses "E" and a grocery tile is detected
        if (currentGroceryTile != null && Input.GetKeyDown(KeyCode.E))
        {
            if (taskList.items.Contains(currentGroceryTile))
            {
                AudioManager.Instance.PlaySFX("PickUP");
                PickUpGrocery();
            }
            else
            {
                Debug.Log("Not in the groceryList");
            }
        }
    }

    private void CheckForPickup()
    {
        Vector3Int playerGridPosition = groceryTilemap.WorldToCell(transform.position);
        frontTilePosition = playerGridPosition + new Vector3Int(playerMovement.FacingDirection.x, playerMovement.FacingDirection.y, 0);

        TileBase tile = groceryTilemap.GetTile(frontTilePosition);

        if (tile != null && tile is GroceryTile groceryTile)
        {
            currentGroceryTile = groceryTile;
            pickUpText.gameObject.SetActive(true);  // Show the pickup text
            pickUpText.text = "[E] \r\nPICK UP:: \n" + groceryTile.groceryName;  // Update the text
        }
        else
        {
            currentGroceryTile = null;
            pickUpText.gameObject.SetActive(false);  // Hide the pickup text if no grocery tile
        }
    }

    private void PickUpGrocery()
    {
        Debug.Log("Pressed E to pick up: " + currentGroceryTile.groceryName);

        // Find the corresponding button image in the TaskList
        Image buttonImage = taskList.GetButtonImageForItem(currentGroceryTile.groceryName);

        // Verify that the current grocery tile's sprite matches the button image
        if (buttonImage != null && buttonImage.sprite == currentGroceryTile.sprite)
        {
            // Reduce the number of items in the task list and verify
            if (taskList.ReduceNumItems(currentGroceryTile.groceryName))
            {
                // If numItem reaches zero, strike through the item
                groceryTilemap.SetTile(frontTilePosition, null);  // Remove the tile from the tilemap
                pickUpText.gameObject.SetActive(false);  // Hide the text after picking up

                // Strike through the item in the task list
                taskList.StrikeThroughItem(currentGroceryTile.groceryName);
            }
            else
            {
                Debug.Log("Still more items left to pick up for " + currentGroceryTile.groceryName);
            }
        }
        else
        {
            Debug.Log("No matching button image found or grocery item does not match.");
        }

        currentGroceryTile = null;  // Clear the current tile reference
    }


}
