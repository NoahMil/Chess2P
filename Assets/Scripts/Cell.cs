using UnityEngine;
using Managers;

public class Cell: MonoBehaviour
{
    public Vector2Int Coordinates { get; set; }
    public Piece Occupant { get; set; }
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
        _collider.enabled = !IsOccupied;
    }

    private void OnMouseDown()
    {
        char columnLetter = (char)('A' + Coordinates.x);
        Debug.Log($"Selected Destination Cell : {columnLetter}{Coordinates.y + 1}");
        GameManager.SelectDestinationCell(this);
    }
}
