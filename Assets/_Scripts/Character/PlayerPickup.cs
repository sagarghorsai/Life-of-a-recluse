using UnityEngine.Tilemaps;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PlayerPickup : MonoBehaviour
{

    public Tilemap groceryTilemap;  // Reference to the grocery Tilemap
    public TextMeshProUGUI pickUpText; // Reference to UI Text for pickup
    public TextMeshProUGUI invalidText; // Reference to UI Text for wrong item text
    private PlayerMovement playerMovement;  // Reference to the PlayerMovement script
    private GroceryTile currentGroceryTile;  // The tile in front of the player (if it's a grocery item)
    private Vector3Int frontTilePosition;  // Position of the tile in front of the player

    public TaskList taskList;  // Reference to the TaskList script
    private AudioManager audioManager; // Reference to the AudioManager script
    private EXPManager expManager;
    float countDown; // Assigns the number/variable for countdown start position.

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // Get PlayerMovement component
        pickUpText.gameObject.SetActive(false);  // Hide pickup text initially
        expManager = FindObjectOfType<EXPManager>();
        countDown = 3;

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
            foreach (var item in taskList.items)
            {


                if (item.groceryName == currentGroceryTile.groceryName)
                {


                    AudioManager.Instance.PlaySFX("PickUP");
                    invalidText.gameObject.SetActive(false); // Hide the invalid text
                    expManager.AddExperience(50);
                    PickUpGrocery();
                    
                    
                }
                else if (item.groceryName != currentGroceryTile.groceryName)
                {
                    Debug.Log($"{item.groceryName} isnt the same as {currentGroceryTile.groceryName}");
                    StartCoroutine(InvalidPickup()); // This is required if you are calling an IEnumerator.
                    
                }
            }
        }

    }

    IEnumerator InvalidPickup()
    {
                    // Debug.Log($"{item.groceryName} isnt the same as {currentGroceryTile.groceryName}");
                    invalidText.gameObject.SetActive(true); // Show the invalid text
                    invalidText.text = "This isn't on your list!"; //Updates the text
                    yield return new WaitForSeconds(3f);
                    invalidText.gameObject.SetActive(false);

                    // if (countDown <= 0)
                   // {
                       // invalidText.gameObject.SetActive(false);
                   // }
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

                // Strike through the item in the task list
                taskList.StrikeThroughItem(currentGroceryTile.groceryName);
            
       

        currentGroceryTile = null;  // Clear the current tile reference
    }


}
