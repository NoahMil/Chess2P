using UnityEngine;
using Pieces;

public class Cell
{
    public Coordinates Coordinates { get; set; }
    public Piece Occupant { get; set; }
    
    public Cell(GameObject prefab, Transform root, int column, int row)
    {
        Coordinates = new Coordinates(column, row);
        GameObject cell = Object.Instantiate(prefab, Coordinates.World, Quaternion.identity, root);
        CellBehaviour cellBehaviour = cell.GetComponent<CellBehaviour>();
        
        cellBehaviour.SetInternalCell(this);
        cell.name = (char)('A' + column) + (row + 1).ToString();
    }
}
