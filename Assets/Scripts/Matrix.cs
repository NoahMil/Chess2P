using System.Collections.Generic;
using UnityEngine;
using Pieces;

public class Matrix
{
    public const int BoardSize = 8;
    private static readonly Cell[,] Grid = new Cell[BoardSize, BoardSize];

    public Matrix(GameObject cellPrefab, Transform cellsRoot)
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

        return Grid[column, row];
    }
    
    public static Cell GetCell(int column, int row)
    {
        return Grid[column, row];
    }
}
