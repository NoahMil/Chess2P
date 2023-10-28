using System.Collections.Generic;
using UnityEngine;

public static class Matrix
{
    public const int BOARD_SIZE = 8;
    private static readonly Cell[,] Grid = new Cell[BOARD_SIZE, BOARD_SIZE];

    public static void Init(GameObject cellPrefab, Transform cellsRoot)
    {
        for (int column = 0; column < BOARD_SIZE; column++)
        {
            for (int row = 0; row < BOARD_SIZE; row++)
            {
                Grid[column, row] = new Cell(cellPrefab, cellsRoot, column, row);
            }
        }
    }

    public static Cell GetCell(string cellName)
    {
        char columnLetter = cellName[0];
        int row = int.Parse(cellName[1..]) - 1;
        int column = columnLetter - 'A';

        if (column < 0 || column > 7) return null;
        if (row < 0 || row > 7) return null;

        return Grid[column, row];
    }
    
    public static Cell GetCell(int column, int row)
    {
        if (column is < 0 or > 7) return null;
        if (row is < 0 or > 7) return null;
        
        return Grid[column, row];
    }

    public static List<Cell> GetAllCells()
    {
        List<Cell> allCells = new();
        
        for (int column = 0; column < BOARD_SIZE; column++)
        {
            for (int row = 0; row < BOARD_SIZE; row++)
            {
                allCells.Add(Grid[column, row]);
            }
        }

        return allCells;
    }

    public static List<Cell> GetMoves(Cell cell)
    {
        return cell.Occupant.GetAvailableMoves(cell);
    }

    public static void ResetCellsTargetState()
    {
        for (int column = 0; column < BOARD_SIZE; column++)
        {
            for (int row = 0; row < BOARD_SIZE; row++)
            {
                Grid[column, row].Behaviour.IsTargetable(Grid[column, row].IsOccupied);
            }
        }
    }
}
