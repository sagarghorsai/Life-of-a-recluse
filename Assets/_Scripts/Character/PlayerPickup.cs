using UnityEngine;
using UnityEngine.Tilemaps;

public class GroceryPickup : MonoBehaviour
{
    public Tilemap groceryTilemap;  // Reference to the grocery Tilemap
    private PlayerMovement playerMovement;  // Reference to the PlayerMovement script

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // Get PlayerMovement component
    }

    void Update()
    {
        CheckForPickup();
    }

    private void CheckForPickup()
    {
        Vector3Int playerGridPosition = groceryTilemap.WorldToCell(transform.position);
        Vector3Int frontTilePosition = playerGridPosition + new Vector3Int(playerMovement.FacingDirection.x, playerMovement.FacingDirection.y, 0);

        Debug.Log("Player Grid Position: " + playerGridPosition);
        Debug.Log("Front Tile Position: " + frontTilePosition);

        TileBase tile = groceryTilemap.GetTile(frontTilePosition);

        if (tile != null)
        {
            Debug.Log("Tile found at position: " + frontTilePosition);
            Debug.Log("Tile type: " + tile.GetType()); // Log the type of the found tile
            if (tile is GroceryTile groceryTile)
            {
                Debug.Log("Picked up: " + groceryTile.groceryName);
                groceryTilemap.SetTile(frontTilePosition, null); // Optionally remove the tile
            }
            else
            {
                Debug.Log("Tile found, but it's not a GroceryTile. It is a: " + tile.GetType());
            }
        }
        else
        {
            Debug.Log("No tile found at position: " + frontTilePosition);
        }
    }
}
