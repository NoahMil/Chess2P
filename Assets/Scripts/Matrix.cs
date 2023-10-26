using System.Collections.Generic;
using UnityEngine;

public static class Matrix
{
    public const int BoardSize = 8;
    private static readonly Cell[,] Grid = new Cell[BoardSize, BoardSize];

    public static void Init(GameObject cellPrefab, Transform cellsRoot)
    {
        for (int column = 0; column < BoardSize; column++)
        {
            for (int row = 0; row < BoardSize; row++)
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
        if (column < 0 || column > 7) return null;
        if (row < 0 || row > 7) return null;
        
        return Grid[column, row];
    }

    public static List<Cell> GetMoves(Cell cell)
    {
        return cell.Occupant.GetAvailableMoves(cell);
    }

    public static void ResetCellsTargetState()
    {
        for (int column = 0; column < BoardSize; column++)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                Grid[column, row].Behaviour.IsTargetable(false);
            }
        }
    }
}
