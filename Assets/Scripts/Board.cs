using System;
using System.Collections.Generic;
using UnityEngine;
using WIP;

public class Board: MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    private static List<Cell> _cells;

    private const int BoardSize = 8;

    private void Awake()
    {
        InitializeCells();
    }

    private void InitializeCells()
    {
        for (int column = 0; column < BoardSize; column++) {
            for (int row = 0; row < BoardSize; row++)
            {
                GameObject obj = Instantiate(_cellPrefab, new Vector3(row, 0f, column), Quaternion.identity, this.transform);
                Cell cell = obj.GetComponent<Cell>();

                obj.name = $"{(char)('A' + column)}{row + 1}";
                cell.Coordinates = new Vector2Int(column, row);
            }
        }
    }
    /* public static Cell FindCell(Vector2Int coordinates)
    {
        foreach (Cell cell in _cells)
        {
            if (coordinates == cell.Coordinates)
                return cell;
        }

        throw new ArgumentOutOfRangeException($"ERROR: cell at position {coordinates.ToString()} is not valid");
    }
    public static Cell FindCell(int x, int y)
    {
        Vector2Int coordinates = new(x, y);

        foreach (Cell cell in _cells) {
            if (coordinates == cell.Coordinates)
                return cell;
        }

        throw new ArgumentOutOfRangeException($"ERROR: cell at position {coordinates.ToString()} is not valid");
    } */

    /* private void InitializeBoard()
    {
        _cells = CreateCells();
        GetInitialSetup();
    }

    private List<Cell> CreateCells()
    {
        List<Cell> cells = new();

        for (int row = 0; row < BoardSize; row++)
        {
            for (int columns = 0; columns < BoardSize; columns++)
            {
                cells.Add(new Cell(row, columns, null));
            }
        }

        return cells;
    }

    private void GetInitialSetup()
    {
        List<Piece> pieces = new List<Piece>(GetComponentsInChildren<Piece>());

        foreach (Piece piece in pieces)
        {
            Vector3 worldPosition = piece.gameObject.transform.position;
            Cell cell = FindCell((int)worldPosition.x, (int)worldPosition.z);
        }
    } */
}
