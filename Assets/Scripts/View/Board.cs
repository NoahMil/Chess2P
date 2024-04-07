using System.Collections.Generic;
using UnityEngine;

using Data;

namespace View
{
    public class Board: MonoBehaviour
    {
        public static Dictionary<string, GameObject> Prefabs;
        
        private static readonly Unit[,] BoardUnit = new Unit[Matrix.BoardSize, Matrix.BoardSize];

        [SerializeField] private GameObject _baseUnit;
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private List<GameObject> _piecesPrefabs;
        
        public void Awake()
        {
            Matrix.Init();
            InitPrefabs();
            Init();
        }

        private void InitPrefabs()
        {
            Prefabs = new Dictionary<string, GameObject>();
        
            foreach (GameObject prefab in _piecesPrefabs)
                Prefabs.Add(prefab.name, prefab);
        }

        private void Init()
        {
            for (int column = 0; column < Matrix.BoardSize; column++)
            {
                for (int row = 0; row < Matrix.BoardSize; row++)
                {
                    Unit unit = InstantiateUnit(column, row);
                    BoardUnit[column, row] = unit;
                }
            }
            
            ResetCellsTargetState();
        }

        private Unit InstantiateUnit(int column, int row)
        {
            Piece matrixPiece = Matrix.GetPiece(column, row);
            
            Unit unit = Instantiate(_baseUnit, this.transform).GetComponent<Unit>();
            unit.name = (char)('A' + column) + (row + 1).ToString();
                    
            unit.Coordinates = new Coordinates(column, row);
            unit.Piece = PieceBehaviour.Create(unit, matrixPiece);
            unit.Cell = CellBehaviour.Create(unit, _cellPrefab);

            return unit;
        }

        public void UpdateBoardView(Piece[,] grid)
        {
            for (int column = 0; column < Matrix.BoardSize; column++)
            {
                for (int row = 0; row < Matrix.BoardSize; row++)
                {
                    Unit unit = GetUnitBehaviour(column, row);
                    
                    Destroy(unit.gameObject);
                    BoardUnit[column, row] = InstantiateUnit(column, row);
                }
            }
        }

        #region Utils

        public static Unit GetUnitBehaviour(Coordinates coords)
        {
            return BoardUnit[coords.Column, coords.Row];
        }
        
        public static Unit GetUnitBehaviour(int column, int row)
        {
            return BoardUnit[column, row];
        }

        #endregion

        #region Fluff

        public static void ResetCellsTargetState()
        {
            for (int column = 0; column < Matrix.BoardSize; column++)
            {
                for (int row = 0; row < Matrix.BoardSize; row++)
                {
                    BoardUnit[column, row].Cell.IsTargetable = false;
                }
            }
        }

        #endregion
    }
}
