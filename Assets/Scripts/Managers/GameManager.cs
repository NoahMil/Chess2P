using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static Side currentPlayerTurn = Side.Light;
        public static bool checkmate = false;

        private static Piece _pieceSelected;
        private static Cell _cellSelected;

        private Side NextTurn => (currentPlayerTurn == Side.Light ? Side.Dark : Side.Light);
        private bool HasPieceSelected => _pieceSelected != null;
        private bool HasCellSelected => _cellSelected != null;

        private bool _showedOnce;

        private void Update()
        {
            if (!_showedOnce) {
                _showedOnce = true;
                Debug.Log($"PlayerTurn: {currentPlayerTurn.ToString()}");
            }

            if (checkmate || Input.GetKeyDown(KeyCode.Escape))
                EditorApplication.ExitPlaymode();
            
            if (!HasPieceSelected) return;
            if (!HasCellSelected) return;
                
            _pieceSelected.Move(_cellSelected);
            SwitchTurn();
        }

        /* private void StartPlayerTurn(Side player)
        {
            _currentPlayerTurn = player;
            Debug.Log($"PlayerTurn: {_currentPlayerTurn.ToString()}");

            if (Checkmate || Input.GetKeyDown(KeyCode.Escape))
                EditorApplication.ExitPlaymode();
            
            if (!HasPieceSelected) return;
            if (!HasCellSelected) return;
                
            _pieceSelected.Move(_cellSelected);
            ResetSelection();
            StartPlayerTurn(NextTurn);
        } */

        private void SwitchTurn()
        {
            currentPlayerTurn = NextTurn;
            _pieceSelected = null;
            _cellSelected = null;
            _showedOnce = false;
        }

        public static void SelectPieceToPlay(Piece piece)
        {
            if (piece.Side != currentPlayerTurn)
            {
                Debug.Log($"This is an opponent Piece. Choose a {currentPlayerTurn.ToString()} Piece !");
                return;
            }

            _pieceSelected = piece;
        }

        public static void SelectDestinationCell(Cell cell)
        {
            if (_pieceSelected != null)
                _cellSelected = cell;
        }
    }
}
