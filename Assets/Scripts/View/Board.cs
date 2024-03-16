using System;
using System.Collections.Generic;
using Data;
using UnityEngine;

using Managers;
using Pieces;

namespace View
{
    public class Board: MonoBehaviour
    {
        [SerializeField] private Transform _cellsRoot;
        [SerializeField] private Transform _piecesRoot;
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private List<GameObject> _piecesPrefabs;

        public static readonly List<CellBehaviour> CellBehaviours = new ();
        public static List<PieceBehaviour> PieceBehaviours = new ();

        private void Start()
        {
            InitializePiecesPrefabs();
            InitializeCells();
            InitializePieces();
            
            foreach (Transform cell in _cellsRoot)
                CellBehaviours.Add(cell.GetComponent<CellBehaviour>());

            foreach (Transform piece in _piecesRoot)
                PieceBehaviours.Add(piece.GetComponent<PieceBehaviour>());
            
            ResetCellsTargetState();
        }

        private void InitializeCells() //TODO: Refactor
        {
            Matrix.Init();
            CellBehaviour.InitBoard(_cellPrefab, _cellsRoot);
        }

        private void InitializePieces() //TODO: Refactor
        {
            string[] pieceOrder = { "Rook", "Knight", "Bishop", "Queen", "King", "Bishop", "Knight", "Rook" };
        
            for (int column = 0; column < Matrix.BoardSize; column++)
            {
                Matrix.GetCell(column, 0).Occupant = Piece.Create("Light" + pieceOrder[column], Matrix.GetCell(column, 0));
                Matrix.GetCell(column, 1).Occupant = Piece.Create("LightPawn", Matrix.GetCell(column, 1));
                Matrix.GetCell(column, 7).Occupant = Piece.Create("Dark" + pieceOrder[column],  Matrix.GetCell(column, 7));
                Matrix.GetCell(column, 6).Occupant = Piece.Create("DarkPawn", Matrix.GetCell(column, 6));
                
                PieceBehaviour.Create(Matrix.GetCell(column, 0), Piece.Prefabs["Light" + pieceOrder[column]], _piecesRoot);
                PieceBehaviour.Create(Matrix.GetCell(column, 1), Piece.Prefabs["LightPawn"], _piecesRoot);
                PieceBehaviour.Create(Matrix.GetCell(column, 7), Piece.Prefabs["Dark" + pieceOrder[column]], _piecesRoot);
                PieceBehaviour.Create(Matrix.GetCell(column, 6), Piece.Prefabs["DarkPawn"], _piecesRoot);
            }
        }

        private void InitializePiecesPrefabs()
        {
            Piece.Prefabs = new Dictionary<string, GameObject>();
        
            foreach (GameObject prefab in _piecesPrefabs)
                Piece.Prefabs.Add(prefab.name, prefab);
        }

        public static void EnableCellsTargets(List<Cell> availableCells)
        {
            if (availableCells == null) throw new NullReferenceException("Error: availableCells list isn't initialized, is there a probleme with <GetAvailableMoves> ?");

            if (availableCells.Count == 0)
            {
                GameManager.ShowNoMovesAvailable();
                return;
            }

            foreach (Cell cell in availableCells)
            {
                GameManager.GetBehaviourCell(cell).IsTargetable(true);
                GameManager.GetBehaviourCell(cell).Highlight(true);
            }
        }

        private static void ResetCellsTargetState()
        {
            Matrix.ResetCellsTargetState();
        }
    
        public static void UpdateView()
        {
            List<Cell> cells = Matrix.GetAllCells();
        
            foreach (Cell cell in cells)
            {
                if (cell.IsOccupied)
                    GameManager.GetBehaviourCell(cell).Occupant.transform.position = cell.Coordinates.World;
            }
        }
    }
}
