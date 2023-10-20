using System.Collections.Generic;
using UnityEngine;
using Helpers;

public class Board: MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform _cellsRoot;
    [SerializeField] private Transform _piecesRoot;
    
    private const int BoardSize = 8;

    private Cell[,] _matrix = new Cell[BoardSize, BoardSize];
    
    private void Awake()
    {
        InitializeBoardMatrix();
    }

    private void InitializeBoardMatrix()
    {
        for (int column = 0; column < BoardSize; column++)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                _matrix[column, row] = new Cell(_cellPrefab, _cellsRoot, column, row);
            }
        }
    }
}
