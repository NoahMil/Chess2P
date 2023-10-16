using System.Collections.Generic;
using UnityEngine;

public class Board: MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform _pieceRoot;
    
    public static readonly List<Cell> Cells = new();

    private const int BoardSize = 8;

    private void Awake()
    {
        InitializeCells();
        AttributePiecesToCells();
    }

    private void InitializeCells()
    {
        for (int column = 0; column < BoardSize; column++)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                GameObject obj = Instantiate(_cellPrefab, new Vector3(row, 0f, column), Quaternion.identity, this.transform);
                Cell cell = obj.GetComponent<Cell>();

                obj.name = $"{(char)('A' + column)}{row + 1}";
                cell.Coordinates = new Vector2Int(column, row);
                Cells.Add(cell);
            }
        }
    }

    private void AttributePiecesToCells()
    {
        List<Piece> pieces = new (_pieceRoot.GetComponentsInChildren<Piece>());

        foreach (Piece piece in pieces)
        {
            Vector3 pieceWorldCoords = piece.gameObject.transform.position;
            Vector2Int pieceCoords = new((int)pieceWorldCoords.z, (int)pieceWorldCoords.x);
        
            foreach (Cell cell in Cells)
            {
                if (pieceCoords != cell.Coordinates) continue;
            
                cell.Occupant = piece;
                piece.CurrentCell = cell;
                break;
            }
        }
    }
}
