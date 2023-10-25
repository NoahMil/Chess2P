using System.Collections.Generic;
using Pieces;
using UnityEngine;

public class Board: MonoBehaviour
{
    [SerializeField] private GameObject _cellPrefab;
    [SerializeField] private Transform _cellsRoot;
    [SerializeField] private Transform _piecesRoot;
    [SerializeField] private List<GameObject> _piecesPrefabs;
    
    private Matrix _matrix;
    private List<PieceBehaviour> _piecesBehaviours;

    private void Start()
    {
        InitializePiecesPrefabs();
        InitialzeMatrix();
        InitializePieces();
    }

    private void InitialzeMatrix()
    {
        _matrix = new Matrix(_cellPrefab, _cellsRoot);
    }

    private void InitializePieces()
    {
        string[] pieceOrder = { "Rook", "Knight", "Bishop", "Queen", "King", "Bishop", "Knight", "Rook" };
        
        for (int column = 0; column < Matrix.BoardSize; column++)
        {
            Matrix.GetCell(column, 0).Occupant = Piece.Create("Light" + pieceOrder[column], Matrix.GetCell(column, 0), _piecesRoot);
            Matrix.GetCell(column, 1).Occupant = Piece.Create("LightPawn", Matrix.GetCell(column, 1), _piecesRoot);
            Matrix.GetCell(column, 7).Occupant = Piece.Create("Dark" + pieceOrder[column],  Matrix.GetCell(column, 7), _piecesRoot);
            Matrix.GetCell(column, 6).Occupant = Piece.Create("DarkPawn", Matrix.GetCell(column, 6), _piecesRoot);
        }
    }

    private void InitializePiecesPrefabs()
    {
        Piece.prefabs = new Dictionary<string, GameObject>();
        
        foreach (GameObject prefab in _piecesPrefabs)
            Piece.prefabs.Add(prefab.name, prefab);
    }

    public static void UpdateView()
    {
        
    }
}
