using System;
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

        if (column is < 0 or > 7) return null;
        if (row is < 0 or > 7) return null;

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
        
        for (int column = 0; column < BoardSize; column++)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                allCells.Add(Grid[column, row]);
            }
        }

        return allCells;
    }

    public static List<Cell> GetPieceCells(Side side)
    {
        List<Cell> pieceCells = new();
        
        for (int column = 0; column < BoardSize; column++)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                Cell cell = Grid[column, row];
                if (cell.IsOccupied && cell.Occupant.Side == side)
                    pieceCells.Add(cell);
            }
        }

        return pieceCells;
    }

    public static Cell GetKing(Side side)
    {
        Cell cell;
        
        for (int column = 0; column < BoardSize; column++)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                cell = Grid[column, row];
                if (cell.IsOccupied && cell.Occupant.IsTheKing && cell.Occupant.Side == side)
                    return cell;
            }
        }

        throw new NullReferenceException($"Error: Unable to get the {side.ToString()} King. A piece may have bypassed all safeguard and took the King out");
    }

    public static List<Cell> GetMoves(Cell cell)
    {
        return cell.Occupant.GetAvailableMoves(cell);
    }

    public static Cell[,] GetCurrentGridSnapshot() // Deep Copy
    {
        Cell[,] snapshot = new Cell[BoardSize, BoardSize];

        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                snapshot[row, col] = new Cell(Grid[row, col]);
            }
        }

        return snapshot;
    }

    public static Cell[,] DuplicateSnapshot(Cell[,] snapshot)
    {
        Cell[,] duplicate = new Cell[BoardSize, BoardSize];

        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                duplicate[row, col] = new Cell(snapshot[row, col]);
            }
        }

        return duplicate;
    }

    public static void ResetCellsTargetState()
    {
        for (int column = 0; column < BoardSize; column++)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                Grid[column, row].Behaviour.IsTargetable(Grid[column, row].IsOccupied);
            }
        }
    }
}
