using UnityEngine;
using Pieces;

public class Cell
{
    public Coordinates Coordinates { get; set; }
    public Piece Occupant { get; set; }
    
    public Cell(GameObject prefab, Transform root, int column, int row)
    {
        Coordinates = new Coordinates(column, row);
        Object.Instantiate(prefab, Coordinates.World, Quaternion.identity, root);
    }
}
