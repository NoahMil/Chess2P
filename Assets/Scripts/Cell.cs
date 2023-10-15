using System;
using UnityEngine;

public class Cell: MonoBehaviour
{
    public Vector2Int Coordinates { get; set; }

    private Vector3 _worldPos;

    private void Awake()
    {
        _worldPos = transform.position;
        Coordinates = new Vector2Int((int)_worldPos.x, (int)_worldPos.z);
    }

    private void OnMouseDown()
    {
        char columnLetter = (char)('A' + Coordinates.x);
        Debug.Log($"Selected: Cell {columnLetter}{Coordinates.y + 1}");
    }
}
