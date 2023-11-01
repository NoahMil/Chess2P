using UnityEngine;

using MonoBehaviours;
using Pieces;

public class Cell
{
    public CellBehaviour Behaviour { get; set; }
    public Coordinates Coordinates { get; set; }
    public Piece Occupant { get; set; }

    public bool IsOccupied => Occupant != null;

    public Cell(GameObject prefab, Transform root, int column, int row)
    {
        Coordinates = new Coordinates(column, row);
        GameObject cell = Object.Instantiate(prefab, Coordinates.World, Quaternion.identity, root);

        Behaviour = cell.GetComponent<CellBehaviour>();
        cell.name = (char)('A' + column) + (row + 1).ToString();
    }
}
