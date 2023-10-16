using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static bool Checkmate = false;
        
        private static Side _currentPlayerTurn = Side.Light;
        private static Piece _pieceSelected;
        private static Cell _cellSelected;

        private Side NextTurn => (_currentPlayerTurn == Side.Light ? Side.Dark : Side.Light);
        private bool HasPieceSelected => _pieceSelected != null;
        private bool HasCellSelected => _cellSelected != null;

        private void Start()
        {
            StartPlayerTurn(_currentPlayerTurn);
        }

        private void StartPlayerTurn(Side player)
        {
            Debug.Log($"PlayerTurn: {_currentPlayerTurn.ToString()}");

            while (!Checkmate || !Input.GetKeyDown(KeyCode.Escape))
            {
                if (!HasPieceSelected) continue;
                if (!HasCellSelected) continue;
                
                _pieceSelected.Move(_cellSelected);
                StartPlayerTurn(NextTurn);
            }
        }

        public static void SelectPieceToPlay(Piece piece)
        {
            if (piece.Side != _currentPlayerTurn)
            {
                Debug.Log($"This is an opponent Piece. Choose a {_currentPlayerTurn.ToString()} Piece !");
                return;
            }

            _pieceSelected = piece;
        }

        public static void SelectDestinationCell(Cell cell)
        {
            _cellSelected = cell;
        }
    }
}
