using System;
using UnityEngine;

public class Cell: MonoBehaviour
{
    public Vector2Int Coordinates { get; set; }
    public Piece Occupant
    {
        get => _occupant;
        set => AttributeOccupant(value);
    }
    public bool IsOccupied => Occupant != null;

    private Piece _occupant;
    private Vector3 _worldPos;
    private Collider _collider;

    private void Awake()
    {
        _worldPos = transform.position;
        _collider = gameObject.GetComponent<Collider>();
        Coordinates = new Vector2Int((int)_worldPos.x, (int)_worldPos.z);
    }

    private void FixedUpdate()
    {
        if (IsOccupied && _collider.enabled)
            _collider.enabled = false;
        else if (!IsOccupied && !_collider.enabled)
            _collider.enabled = true;
    }

    private void OnMouseDown()
    {
        char columnLetter = (char)('A' + Coordinates.x);
        Debug.Log($"Selected: Cell {columnLetter}{Coordinates.y + 1}");
    }

    private static void AttributeOccupant(Piece piece)
    {
        foreach (Cell cell in Board.Cells)
        {
            // if (cell.Coordinates )
        }
    }
}
