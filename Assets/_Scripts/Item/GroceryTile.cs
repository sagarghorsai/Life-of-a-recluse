using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Grocery Tile", menuName = "Tiles/Grocery Tile")]
public class GroceryTile : Tile
{
    public string groceryName;  // To hold the name of the grocery
    public TileType tileType;
}
public enum TileType
{
    Grocery,
    Flower,
    Bakery,
    Meat,
    Shelving,
}