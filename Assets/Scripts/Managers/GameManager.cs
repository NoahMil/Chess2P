using System.Collections.Generic;
using UnityEngine;

using MonoBehaviours;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static Side CurrentPlayerTurn { get; private set; }
        public static Side OpponentTurn => (CurrentPlayerTurn == Side.Light ? Side.Dark : Side.Light);
        public static bool Checkmate { get; set; }
        public static bool Check { get; set; }
        
        private static Cell _origin;
        private static Cell _destination;
        private static List<Cell> _moves;
        private static bool _updateOnce;

        private static List<Cell> _checkResponsibles = new ();
        private static List<Cell> _invalidCellsForKing = new ();
        private static List<Cell> _validEscapeDestinations = new ();

        private bool _confirmEscape;
        private float _escapeTimer = 3f;

        #region Interactions
        
        private void Awake()
        {
            CurrentPlayerTurn = Side.Light;
            Checkmate = false;
        }

        private void Update()
        {      
            if (!_updateOnce) {
                UIManager.UpdateTurn(CurrentPlayerTurn);
                _updateOnce = true;
            }
                
            if (Input.GetButtonDown("Fire2"))
                Unselect();

            EmergencyEscape(Input.GetKeyDown(KeyCode.Escape));
        }

        private void EmergencyEscape(bool triggered)
        {
            if (triggered && _confirmEscape)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
#endif
                Application.Quit();
            }
                
            if (triggered) {
                _confirmEscape = true;
            }

            if (_escapeTimer <= 0f)
                _escapeTimer = 3f;

            _escapeTimer -= Time.deltaTime;
        }
        
        #endregion

        #region Game Logic
        
        #region Cell Selection
            
        public static void SelectCell(Cell cell)
        {
            if (Check)
                CheckSelection(cell);
            else
                RegularSelection(cell);
        }

        private static void CheckSelection(Cell cell)
        {
            List<Cell> validMoves = GatherValidMoves();
                    
            if (_origin == null && validMoves.Contains(cell)) // If no piece have been selected, checks if the current selected can be played;
                SetOrigin(cell);
            else if (_origin != null && _validEscapeDestinations.Contains(cell)) // If a piece have been selected to be played, verify if the destination is in the list that covers the check;
                SetDestination(cell);
            else
            {
                Debug.Log($"{CurrentPlayerTurn.ToString()} King is in Check. No moves from this piece can escape it.");
                Unselect();
            }
        }

        private static void RegularSelection(Cell cell)
        {
            if (_origin == null)
            {
                SetOrigin(cell);
            }
            else if (_origin != null && IsDifferentPieceSelected(cell))
            {
                Unselect();
                SelectCell(cell); // Tail Recursion
            }
            else
            {
                SetDestination(cell);
            }
        }
                
        private static void Unselect()
        {
            if (_origin == null) return;
                    
            _origin.Occupant.Behaviour.Highlight(false);
            _origin.Occupant.Behaviour.HighlightError(false);
            _origin = null;

            if (_moves == null) return;

            foreach (Cell cell in _moves)
                cell.Behaviour.Highlight(false);
            _moves = null;
        }
            
        #endregion

        private static void ResolveMovement()
        {
            _origin.Occupant.Behaviour.Highlight(false);
                
            if (_destination is { IsOccupied: true } && _destination.Occupant.Side == OpponentTurn)
                Destroy(_destination.Occupant.Behaviour.gameObject);

            _destination.Occupant = _origin.Occupant;
            _destination.Occupant.Cell = _destination; // I need to update the Piece's internal reference to it's holding Cell. Since the AvailableMove works automously (without re-providing the cell as a paramater)
            _destination.Occupant.HasMoved = true;
            _origin.Occupant = null;

            Debug.Log($"Played {_destination.GetType()} from {_origin.Name} to {_destination.Name}");
                
            Board.UpdateView();
            ChangeTurn();
        }

        private static void ChangeTurn()
        {
            _updateOnce = false;
            _destination = null;
            _origin = null;

            foreach (Cell cell in _moves) // Reset Cells highlighting
                cell.Behaviour.Highlight(false);
            _moves = null;
                
            CurrentPlayerTurn = OpponentTurn;
            Check = CurrentPlayerIsInCheck();
        }

        #endregion

        #region Targets

        private static void SetOrigin(Cell cell)
        {
            if (!cell.IsOccupied) return;
            if (cell.Occupant.Side != CurrentPlayerTurn) {
                Debug.Log("You can't play an Opponent piece !");
                return;
            }
                    
            _origin = cell;
            _origin.Occupant.Behaviour.Highlight(true);
                    
            _moves = Matrix.GetMoves(_origin);
            Board.EnableCellsTargets(_moves);
        }
            
        private static void SetDestination(Cell cell)
        {
            if (!_moves.Contains(cell)) return;
                    
            _destination = cell;
            ResolveMovement();
        }

        #endregion
        
        #region Checks handling

        private static bool CurrentPlayerIsInCheck()
        {
            Cell king = Matrix.GetKing(CurrentPlayerTurn);
            List<Cell> opponentPieces = Matrix.GetPieceCells(OpponentTurn);

            ClearChecks();

            foreach (Cell opponentCell in opponentPieces) // For each opponent pieces
            {
                List<Cell> possibleMoves = opponentCell.Occupant.AvailableMoves();

                foreach (Cell target in possibleMoves) // For each possibility
                {
                    if (target == king) // If one of them threaten the King
                    {
                        Debug.Log($"{opponentCell.Occupant.Side} {opponentCell.Occupant.GetType().Name} make the {king.Occupant.Side} {king.Occupant.GetType().Name} in check, tracking down...");
                        _checkResponsibles.Add(opponentCell); // Track the piece
                        break;
                    }
                }

                if (_checkResponsibles.Count > 0) // If any opponent piece threaten the King
                {
                    Debug.Log($"{_checkResponsibles.Count} threats have been detected towards {king.Occupant.Side} {king.Occupant.GetType().Name}");
                    foreach (Cell responsible in _checkResponsibles) // For each King's threats
                    {
                        foreach (Cell cell in responsible.Occupant.AvailableMoves())  
                        {
                            cell.Behaviour.HighlightCheck(true);; // Highlight the threat line(s) 
                            _invalidCellsForKing.Add(cell); // and track cells as invalid to play. Use later to extract possible moves for the King
                        }
                    }

                    return true;
                }
            }

            return false;
        }
            
        private static void ClearChecks()
        {
            foreach (Cell cell in _checkResponsibles)
            {
                cell.Behaviour.Highlight(false);
            }
                
            _checkResponsibles.Clear();
            _invalidCellsForKing.Clear();
        }
            
        private static List<Cell> GatherValidMoves()
        {
            List<Cell> availableMoves = new();
            Cell[,] snapshot = Matrix.GetCurrentGridSnapshot();

            foreach (Cell cell in snapshot) // For each piece from current player' side
            {
                if (cell.Occupant == null || cell.Occupant.Side != CurrentPlayerTurn) continue;
                
                foreach (Cell moves in cell.Occupant.AvailableMoves()) // For each moves this piece can achieve
                {
                    // Simulate a new grid (from the snapshot) by manually move the piece according to each of it's positions
                    VirtualResolve(snapshot, cell);
                    
                    // Verify if there is any checks left
                    // if (Checks) -> move not valid; continue;
                    // else        -> move does resolve one or all the checks; Add move to valid list; m++
                    
                }
                // if (m == 0) -> This piece can't resolve the Check(s); Iterate next piece;
            }
            
            // if (availableMoves.Count == 0) -> No piece can't be played; Checkmate;
            // else
            return availableMoves; // Maybe push Checkmate verification upward;
        }

        private static void VirtualResolve(Cell[,] snapshot, Cell moveToTest) // Resolve every possible movements from a snapshot
        {
            Cell[,] virtualGrid = Matrix.DuplicateSnapshot(snapshot); // Preserve state for each piece' simulation
            Cell virtualOrigin;
            Cell virtualDestination;
            
            
        }

        #endregion

        #region Utils

        private static bool IsDifferentPieceSelected(Cell cell) {
            return cell.IsOccupied && cell.Occupant.Side == CurrentPlayerTurn;
        }

        public static void ShowNoMovesAvailable() {
            _origin.Occupant.Behaviour.HighlightError(true);
        }

        #endregion
    }
}
