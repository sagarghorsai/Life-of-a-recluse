using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileRandomizer : MonoBehaviour
{
    [Header("---------- TileMap References ----------")]
    public Tilemap inFrontTile; // Assign this in the Inspector
    public Tilemap behindTile; // Assign this in the Inspector

    public Tilemap interactorTilemap; // The Tilemap where both grocery and flower tiles will be placed
    [Header("---------- GroceryTiles ----------")]
    public TileBase[] groceryPlacementTiles; // Array of grocery placement tiles
    public TileBase[] groceryTiles; // Array of grocery tiles to place

    [Header("---------- FlowerTiles ----------")]
    public TileBase[] flowerPlacementTiles; // Array of flower placement tiles
    public TileBase[] flowerTiles; // Array of flower tiles to place

    [Header("---------- LeftShelvingTiles ----------")]
    public TileBase[] leftShelvingPlacementTiles; // Array of shelving placement tiles
    public TileBase[] leftShelvingTiles; // Array of shelving tiles to place

    [Header("---------- RightShelvingTiles ----------")]
    public TileBase[] rightShelvingPlacementTiles; // Array of shelving placement tiles
    public TileBase[] rightShelvingTiles; // Array of shelving tiles to place

    [Header("---------- BakeryTiles ----------")]
    public TileBase[] bakeryPlacementTiles; // Array of bakery Placement Tiles
    public TileBase[] bakeryTiles; // Array of Bakery Tiles to place

    [Header("---------- MeatTiles ----------")]
    public TileBase[] meatPlacementTiles; // Array of bakery Placement Tiles
    public TileBase[] meatTiles; // Array of Bakery Tiles to place



    void Start()
    {
        PlaceGroceryTiles();
        PlaceFlowerTiles();
        PlaceLeftShelvingTiles();
        PlaceRightShelvingTiles();
        PlaceBakeryTiles();
        PlaceMeatTiles();
    }

    void PlaceBakeryTiles()
    {
        List<Vector3Int> availablePositions = GetAvailablePositions(bakeryPlacementTiles);

        // Shuffle available positions to randomize placement
        Shuffle(availablePositions);

        // Place all bakery tiles in the available positions
        int bakeryTileCount = bakeryTiles.Length;
        int positionsCount = availablePositions.Count;

        for (int i = 0; i < bakeryTileCount && i < positionsCount; i++)
        {
            interactorTilemap.SetTile(availablePositions[i], bakeryTiles[i]);
            Debug.Log($"Placed bakery tile at {availablePositions[i]}: {bakeryTiles[i].name}");
        }

        // If there are still available positions left, place additional bakery tiles randomly
        int extraTilesPlaced = 0;
        for (int i = bakeryTileCount; i < positionsCount; i++)
        {
            TileBase tileToPlace = bakeryTiles[Random.Range(0, bakeryTiles.Length)];
            interactorTilemap.SetTile(availablePositions[i], tileToPlace);
            Debug.Log($"Placed extra bakery tile at {availablePositions[i]}: {tileToPlace.name}");
            extraTilesPlaced++;
        }

        Debug.Log($"Total extra bakery tiles placed: {extraTilesPlaced}");
    }

    void PlaceMeatTiles()
    {
        List<Vector3Int> availablePositions = GetAvailablePositions(meatPlacementTiles);

        // Shuffle available positions to randomize placement
        Shuffle(availablePositions);

        // Place all meat tiles in the available positions
        int meatTileCount = meatTiles.Length;
        int positionsCount = availablePositions.Count;

        for (int i = 0; i < meatTileCount && i < positionsCount; i++)
        {
            interactorTilemap.SetTile(availablePositions[i], meatTiles[i]);
            Debug.Log($"Placed meat tile at {availablePositions[i]}: {meatTiles[i].name}");
        }

        // If there are still available positions left, place additional meat tiles randomly
        int extraTilesPlaced = 0;
        for (int i = meatTileCount; i < positionsCount; i++)
        {
            TileBase tileToPlace = meatTiles[Random.Range(0, meatTiles.Length)];
            interactorTilemap.SetTile(availablePositions[i], tileToPlace);
            Debug.Log($"Placed extra meat tile at {availablePositions[i]}: {tileToPlace.name}");
            extraTilesPlaced++;
        }

        Debug.Log($"Total extra meat tiles placed: {extraTilesPlaced}");
    }

    void PlaceLeftShelvingTiles()
    {
        List<Vector3Int> availablePositions = GetAvailablePositions(leftShelvingPlacementTiles);

        // Shuffle available positions to randomize placement
        Shuffle(availablePositions);

        // Place all left shelving tiles in the available positions
        int shelvingTileCount = leftShelvingTiles.Length;
        int positionsCount = availablePositions.Count;

        for (int i = 0; i < shelvingTileCount && i < positionsCount; i++)
        {
            interactorTilemap.SetTile(availablePositions[i], leftShelvingTiles[i]);
        }

        // If there are still available positions left, place additional left shelving tiles randomly
        for (int i = shelvingTileCount; i < positionsCount; i++)
        {
            TileBase tileToPlace = leftShelvingTiles[Random.Range(0, leftShelvingTiles.Length)];
            interactorTilemap.SetTile(availablePositions[i], tileToPlace);
        }

    }

    void PlaceRightShelvingTiles()
    {
        List<Vector3Int> availablePositions = GetAvailablePositions(rightShelvingPlacementTiles);

        // Shuffle available positions to randomize placement
        Shuffle(availablePositions);

        // Place all right shelving tiles in the available positions
        int shelvingTileCount = rightShelvingTiles.Length;
        int positionsCount = availablePositions.Count;

        for (int i = 0; i < shelvingTileCount && i < positionsCount; i++)
        {
            interactorTilemap.SetTile(availablePositions[i], rightShelvingTiles[i]);
        }

        for (int i = shelvingTileCount; i < positionsCount; i++)
        {
            TileBase tileToPlace = rightShelvingTiles[Random.Range(0, rightShelvingTiles.Length)];
            interactorTilemap.SetTile(availablePositions[i], tileToPlace);
        }

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

    }

    // Helper method to get available positions based on the provided placement tiles
    private List<Vector3Int> GetAvailablePositions(TileBase[] placementTiles)
    {
        List<Vector3Int> availablePositions = new List<Vector3Int>();
        BoundsInt bounds = inFrontTile.cellBounds;
        BoundsInt bounds1 = behindTile.cellBounds;

        for (int x = bounds.x; x < bounds.xMax; x++)
        {
            for (int y = bounds.y; y < bounds.yMax; y++)
            {
                Vector3Int currentPos = new Vector3Int(x, y, 0);
                TileBase currentTile = inFrontTile.GetTile(currentPos);

                // Check if the current tile is one of the specified placement tiles
                if (currentTile != null && IsPlacementTile(currentTile, placementTiles))
                {
                    availablePositions.Add(currentPos);
                }
            }
        }

        for (int x = bounds1.x; x < bounds1.xMax; x++)
        {
            for (int y = bounds1.y; y < bounds1.yMax; y++)
            {
                Vector3Int currentPos = new Vector3Int(x, y, 0);
                TileBase currentTile = behindTile.GetTile(currentPos);

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
