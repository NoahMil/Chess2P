using Unity.VisualScripting;
using UnityEngine;

public class Cell
{
    private GameObject _cellPrefab;
    private Coordinates _coordinates;
    private Piece _occupant = null;

    public Coordinates Coordinates => _coordinates;
    public Piece Occupant => _occupant;
    public bool IsOccupied => _occupant != null;

    public Cell(GameObject prefab, Transform root, int column, int row)
    {
        _coordinates = new Coordinates(column, row);

        GameObject newObject = Object.Instantiate(prefab, _coordinates.World, Quaternion.identity, root);
        // BoardCell cell = newObject.GetComponent<BoardCell>();
    }
}