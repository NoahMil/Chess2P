using System;
using System.Collections.Generic;
using UnityEngine;

using Managers;
using Data;
using Enums;

namespace View
{
    public class Board: MonoBehaviour
    {
        [SerializeField] private Transform _cellsRoot;
        [SerializeField] private Transform _piecesRoot;
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private List<GameObject> _piecesPrefabs;

        public static Dictionary<string, GameObject> Prefabs;
        public static List<CellBehaviour> CellBehaviours;
        public static List<PieceBehaviour> PieceBehaviours;

        private void Start()
        {
            Initialize();
            // Matrix.Debug();
        }

        private void Initialize()
        {
            InitializePiecesPrefabs();
            Matrix.Init();
            CellBehaviours = CellBehaviour.InitBoard(_cellPrefab, _cellsRoot);
            PieceBehaviours = PieceBehaviour.InitBoard(_piecesRoot);
            ResetCellsTargetState();
        }

        private void InitializePiecesPrefabs()
        {
            Prefabs = new Dictionary<string, GameObject>();
        
            foreach (GameObject prefab in _piecesPrefabs)
                Prefabs.Add(prefab.name, prefab);
        }

        #region View

        public static void EnableCellsTargets(List<Piece> availableCells)
        {
            if (availableCells == null) throw new NullReferenceException("Error: availableCells list isn't initialized, is there a probleme with <GetAvailableMoves> ?");

            if (availableCells.Count == 0)
            {
                GameManager.ShowNoMovesAvailable();
                return;
            }

            foreach (Piece piece in availableCells)
            {
                GameManager.GetPieceBehaviour(piece).Cell.IsTargetable(true);
                GameManager.GetPieceBehaviour(piece).Cell.Highlight(HighlightType.Active);
            }
        }

        private static void ResetCellsTargetState()
        {
            Matrix.ResetCellsTargetState();
        }
    
        public static void UpdateView()
        {
            List<Piece> pieces = Matrix.GetAllPieces();
        
            foreach (Piece cell in pieces)
            {
                if (!cell.IsEmpty)
                    GameManager.GetPieceBehaviour(cell).Cell.transform.position = cell.Coordinates.World;
            }
        }

        #endregion
    }
}
