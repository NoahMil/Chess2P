using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static Side CurrentPlayerTurn { get; set; }
        public static bool Checkmate { get; set; }
        
        private Side NextTurn => (CurrentPlayerTurn == Side.Light ? Side.Dark : Side.Light);

        private static Cell _selection;
        private static Cell _destination;
        private static List<Cell> _availableMoves;
        private bool _showedOnce;

        private void Awake()
        {
            CurrentPlayerTurn = Side.Light;
            Checkmate = false;
        }

        private void Update()
        {
            if (!_showedOnce) {
                Debug.Log($"Player Turn: {CurrentPlayerTurn.ToString()}");
                _showedOnce = true;
            }

            if (Input.GetButtonDown("Fire2"))
                Unselect();
        }

        private void FixedUpdate()
        {
            if (_selection == null) return;

            _availableMoves = Matrix.GetMoves(_selection);
        }

        public static void SelectCell(Cell cell)
        {
            if (!cell.IsOccupied) return;
            if (cell.Occupant.Side != CurrentPlayerTurn) {
                Debug.Log("You can't play an Opponent piece !");
                return;
            }
            if (_selection != null) {
                Unselect();
            }

            _selection = cell;
            _selection.Occupant.Behaviour.Highlight(true);
        }

        public static void Unselect()
        {
            if (_selection == null) return;
            
            _selection.Occupant.Behaviour.Highlight(false);
            _selection = null;
            _availableMoves = null;
        }
    }
}
