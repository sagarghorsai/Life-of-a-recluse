using UnityEngine.Tilemaps;
using UnityEngine;
using TMPro;

public class GroceryPickup : MonoBehaviour
{

    public Tilemap groceryTilemap;  // Reference to the grocery Tilemap
    public TextMeshProUGUI pickUpText;  // Reference to UI Text for pickup
    private PlayerMovement playerMovement;  // Reference to the PlayerMovement script
    private GroceryTile currentGroceryTile;  // The tile in front of the player (if it's a grocery item)
    private Vector3Int frontTilePosition;  // Position of the tile in front of the player

    public TaskList taskList;  // Reference to the TaskList script
    public AudioManager audioManager; // Reference to the AudioManager script

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
            PickUpGrocery();
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
        groceryTilemap.SetTile(frontTilePosition, null);  // Remove the tile from the tilemap
        pickUpText.gameObject.SetActive(false);  // Hide the text after picking up

        // Notify the TaskList to strike through the item
        taskList.StrikeThroughItem(currentGroceryTile.groceryName);
        currentGroceryTile = null;  // Clear the current tile reference

        AudioManager.Instance.PlaySFX("PickUP");
    }
}
