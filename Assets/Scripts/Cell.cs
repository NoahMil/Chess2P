using UnityEngine;

using MonoBehaviours;
using Pieces;

public class Cell
{
    public CellBehaviour Behaviour { get; set; }
    public Coordinates Coordinates { get; set; }
    public Piece Occupant { get; set; }

    public string Name => Behaviour.name;
    public bool IsOccupied => Occupant != null;

    public Cell(GameObject prefab, Transform root, int column, int row)
    {
        Coordinates = new Coordinates(column, row);
        GameObject cell = Object.Instantiate(prefab, Coordinates.World, Quaternion.identity, root);

        Behaviour = cell.GetComponent<CellBehaviour>();
        cell.name = (char)('A' + column) + (row + 1).ToString();
    }

    public Cell(Cell copy)
    {
        Coordinates = copy.Coordinates;
        Behaviour = copy.Behaviour;
        Occupant = copy.Occupant;
    }
}
