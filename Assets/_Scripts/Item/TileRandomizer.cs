using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRandomizer : MonoBehaviour
{
    [Header("---------- TileMap References ----------")]
    public Tilemap collisionTilemap; // Assign this in the Inspector
    public Tilemap interactorTilemap; // The Tilemap where both grocery and flower tiles will be placed
    [Header("---------- GroceryTiles ----------")]
    public TileBase[] groceryPlacementTiles; // Array of grocery placement tiles
    public TileBase[] groceryTiles; // Array of grocery tiles to place

    [Header("---------- FlowerTiles ----------")]
    public TileBase[] flowerPlacementTiles; // Array of flower placement tiles
    public TileBase[] flowerTiles; // Array of flower tiles to place
 

    void Start()
    {
        PlaceGroceryTiles();
        PlaceFlowerTiles();
    }

    void PlaceGroceryTiles()
    {
        List<Vector3Int> availablePositions = GetAvailablePositions(groceryPlacementTiles);

        // Shuffle available positions to randomize placement
        Shuffle(availablePositions);

        // Place all grocery tiles in the available positions
        int groceryTileCount = groceryTiles.Length;
        int positionsCount = availablePositions.Count;

        for (int i = 0; i < groceryTileCount && i < positionsCount; i++)
        {
            interactorTilemap.SetTile(availablePositions[i], groceryTiles[i]);
        }

        // If there are still available positions left, place additional grocery tiles randomly
        for (int i = groceryTileCount; i < positionsCount; i++)
        {
            TileBase tileToPlace = groceryTiles[Random.Range(0, groceryTiles.Length)];
            interactorTilemap.SetTile(availablePositions[i], tileToPlace);
        }

        Debug.Log($"Placed {groceryTileCount} grocery tiles initially, and {positionsCount - groceryTileCount} additional tiles in the remaining spaces.");
    }

    void PlaceFlowerTiles()
    {
        List<Vector3Int> availablePositions = GetAvailablePositions(flowerPlacementTiles);

        // Shuffle available positions to randomize placement
        Shuffle(availablePositions);

        // Place all flower tiles in the available positions
        int flowerTileCount = flowerTiles.Length;
        int positionsCount = availablePositions.Count;

        for (int i = 0; i < flowerTileCount && i < positionsCount; i++)
        {
            interactorTilemap.SetTile(availablePositions[i], flowerTiles[i]);
        }

        // If there are still available positions left, place additional flower tiles randomly
        for (int i = flowerTileCount; i < positionsCount; i++)
        {
            TileBase tileToPlace = flowerTiles[Random.Range(0, flowerTiles.Length)];
            interactorTilemap.SetTile(availablePositions[i], tileToPlace);
        }

        Debug.Log($"Placed {flowerTileCount} flower tiles initially, and {positionsCount - flowerTileCount} additional tiles in the remaining spaces.");
    }

    // Helper method to get available positions based on the provided placement tiles
    private List<Vector3Int> GetAvailablePositions(TileBase[] placementTiles)
    {
        List<Vector3Int> availablePositions = new List<Vector3Int>();
        BoundsInt bounds = collisionTilemap.cellBounds;

        for (int x = bounds.x; x < bounds.xMax; x++)
        {
            for (int y = bounds.y; y < bounds.yMax; y++)
            {
                Vector3Int currentPos = new Vector3Int(x, y, 0);
                TileBase currentTile = collisionTilemap.GetTile(currentPos);

                // Check if the current tile is one of the specified placement tiles
                if (currentTile != null && IsPlacementTile(currentTile, placementTiles))
                {
                    availablePositions.Add(currentPos);
                }
            }
        }

        return availablePositions;
    }

    // Check if the current tile is in the specified placement tiles
    private bool IsPlacementTile(TileBase tile, TileBase[] placementTiles)
    {
        foreach (TileBase placementTile in placementTiles)
        {
            if (tile == placementTile)
            {
                return true;
            }
        }
        return false;
    }

    // Shuffle a list of Vector3Int positions
    private void Shuffle(List<Vector3Int> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Vector3Int temp = positions[i];
            int randomIndex = Random.Range(i, positions.Count);
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }
    }
}
