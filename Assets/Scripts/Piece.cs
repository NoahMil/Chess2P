using UnityEngine;
using Managers;
using SO;

public class Piece: MonoBehaviour
{
    [SerializeField] private PieceSO _blueprintSO;
    [SerializeField] private Side _side;

    public Side Side => _side;
    public Cell CurrentCell { get; set; }

    private void OnMouseDown()
    {
        Debug.Log($"Selected Piece: {_side} {_blueprintSO.name}");
        if (GameManager.currentPlayerTurn != _side)
        {
            GameManager.SelectDestinationCell(CurrentCell);
        }
        else
        {
            GameManager.SelectPieceToPlay(this);
        }
    }

    public void Move(Cell destinationCell)
    {
        _blueprintSO.Move(this, destinationCell);
    }

    public void UpdatePosition()
    {
        Vector3 newPosition = new(CurrentCell.Coordinates.y, 0, CurrentCell.Coordinates.x);
        gameObject.transform.position = newPosition;
    }
}
