using UnityEngine;

public class Matrix
{
    public const int BoardSize = 8;

    private Cell[,] _grid = new Cell[BoardSize, BoardSize];

    public Matrix(GameObject cellPrefab, Transform cellsRoot)
    {
        for (int column = 0; column < BoardSize; column++)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                _grid[column, row] = new Cell(cellPrefab, cellsRoot, column, row);
            }
        }
    }
}
