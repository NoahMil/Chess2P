using System.Collections.Generic;
using Helpers;
using MonoBehaviours;
using UnityEngine;

namespace Managers
{
    public class CheckObserver: MonoBehaviour
    {
        private static List<Cell> _checkResponsibles = new ();
        private static List<Cell> _invalidCellsForKing = new ();
        
        private void Awake()
        {
            GameManager.onMoveMade += OnMoveMade;
        }

        private void OnDestroy()
        {
            GameManager.onMoveMade -= OnMoveMade;
        }
        
        private void OnMoveMade(Side side) // EventHandler
        {
            Cell king = Matrix.GetKing(side);
            List<Cell> opponentPieces = Matrix.GetPieceCells(side == Side.Light ? Side.Dark : Side.Light);
            
            ClearChecks();

            foreach (Cell opponentCell in opponentPieces) // For each opponent pieces
            {
                List<Cell> possibleMovements = opponentCell.Occupant.GetAvailableMoves(opponentCell); // Get every possible movements

                foreach (Cell target in possibleMovements) // For each possibility
                {
                    if (target == king) // If one of them threaten the King
                    {
                        Debug.Log($">>> >>> {opponentCell.Occupant.Side} {opponentCell.Occupant.GetType()} make the {king.Occupant.Side} {king.Occupant.GetType()} in check, tracking down...");
                        _checkResponsibles.Add(opponentCell); // Track the piece
                        break;
                    }
                }
            }

            if (_checkResponsibles.Count > 0) // If any opponent piece threaten the King
            {
                Debug.Log($"Found {_checkResponsibles.Count} possible checks");
                foreach (Cell responsible in _checkResponsibles) // Highlight the threat line(s) and track cells as invalid to play
                {
                    responsible.Occupant.Behaviour.HighlightError(true, Utility.CheckWarning);
                    foreach (Cell cell in responsible.Occupant.GetPathToKing(responsible)) 
                    {
                        cell.Behaviour.Highlight(true, Utility.PieceCheckWarning);
                        _invalidCellsForKing.Add(cell); // Use later to extract possible moves for the King
                    }
                }
                
            }

            GameManager.Check = false;
            Debug.Log("No checks found...");
        }

        private void ClearChecks()
        {
            foreach (Cell cell in _checkResponsibles)
            {
                cell.Behaviour.Highlight(false, CellBehaviour.defaultColor);
            }
            
            _checkResponsibles.Clear();
            _invalidCellsForKing.Clear();
        }
    }
}
