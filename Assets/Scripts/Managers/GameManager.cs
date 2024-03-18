using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Data;
using Enums;
using View;
using IA;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private static Piece _origin;
        private static Piece _destination;
        private static List<Piece> _moves;
        private static bool _updateOnce;

        private static readonly List<Piece> CheckResponsibles = new ();
        private static readonly Dictionary<Piece, List<Piece>> ValidCheckMoves = new ();
        
        // ---- NEW ---- //
        private static readonly Dictionary<Piece, List<Piece>> ValidMoves = new();
        // ------------ //
        
        private bool _confirmEscape;
        private float _escapeTimer = 3f;
        public static Side CurrentPlayerTurn { get; private set; }
        public static Side OpponentTurn => (CurrentPlayerTurn == Side.Light ? Side.Dark : Side.Light);
        public static bool Checkmate { get; set; }
        public static bool Check { get; set; }

        [ContextMenu("Think")]
        public void Think()
        {
            Node node = new Node(Matrix.Grid, CurrentPlayerTurn);
            
            foreach (Piece piece in node.Grid)
            {
                if (piece == null) continue;
                if (piece.Side != CurrentPlayerTurn) continue;
                
                foreach (Piece availableMove in piece.AvailableMoves(piece.Coordinates))
                {
                    
                }
            }
        }
        
        #region Data Link

        public static Piece GetDataPiece(PieceBehaviour viewPiece)
        {
            return Matrix.GetPiece(viewPiece.Coordinates.Row, viewPiece.Coordinates.Column);
        }

        public static PieceBehaviour GetPieceBehaviour(Piece dataPiece)
        {
            if (dataPiece == null)
                throw new Exception($"Unexistant Cell request in Grid");
                    
            return Board.PieceBehaviours.First(piece => piece.Coordinates.Equals(dataPiece.Coordinates));
        }

        #endregion
        
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

        #region Piece/Cell Selection

        public static void SelectPiece(Piece piece)
        {
            if (Check)
                CheckSelection(piece);
            else
                RegularSelection(piece);
        }

        private static void CheckSelection(Piece piece)
        {
            // If no piece have been selected, checks if the current selected is part of the Pieces of the valid moves Dictionnary
            if (_origin == null && ValidCheckMoves.ContainsKey(piece)) {
                SetOrigin(piece);
            }
            // If a piece have been selected to be played, verify if the destination matches one of the valid moves list from that Piece in the Dictionnary;
            else if (_origin != null && ValidCheckMoves[piece].Contains(piece)) {
                SetDestination(piece);
            } 
            else
            {
                Debug.Log($"{CurrentPlayerTurn.ToString()} King is in Check. No moves from this piece can escape it.");
                Unselect();
            }
        }

        private static void RegularSelection(Piece piece)
        {
            if (_origin == null)
            {
                SetOrigin(piece);
            }
            else if (_origin != null && IsDifferentPieceSelected(piece))
            {
                Unselect();
                SelectPiece(piece); // Tail Recursion
            }
            else
            {
                SetDestination(piece);
            }
        }

        private static void Unselect()
        {
            if (_origin == null) return;
                    
            GetPieceBehaviour(_origin).Highlight(HighlightType.None);
            _origin = null;

            if (_moves == null) return;

            foreach (Piece piece in _moves)
                GetPieceBehaviour(piece).Highlight(HighlightType.None);
            
            _moves = null;
        }

        #endregion

       /* private static void ResolveMovement()
        {
            GetPieceBehaviour(_origin).Highlight(HighlightType.None);
                
            if (_destination is { IsEmpty: false } && _destination.Side == OpponentTurn)
                Destroy(GetPieceBehaviour(_destination).gameObject);

            Coordinates originCoords = _origin.Coordinates;
            _destination = _origin;
            _destination.HasMoved = true;

            _origin.Clear();
            GetPieceBehaviour(_origin).Occupant = null; // Manually drop the _origin Behaviour cell's Occupant to null too;

            Debug.Log($"Played {_destination.GetType()} from {GetPieceBehaviour(_origin).Name} to {GetPieceBehaviour(_destination).Name}");
                
            Board.UpdateView();
            HandleCheck();
            ChangeTurn();
        } */

        private static void HandleCheck()
        {
            if (PlayerIsInCheck(Matrix.GetCurrentGridSnapshot(), OpponentTurn))
            {
                Check = true;
                GatherValidMoves();
                
                if (ValidCheckMoves.Count == 0) { // Chekmate;
                    Debug.LogWarning("No moves can be made -> Checkmate");
                    return;
                }
            }

            Check = false;
        }

        private static void ChangeTurn()
        {
            _updateOnce = false;
            _destination = null;
            _origin = null;

            foreach (Piece piece in _moves) // Reset Cells highlighting
                GetPieceBehaviour(piece).Highlight(HighlightType.None);
            _moves = null;
                
            CurrentPlayerTurn = OpponentTurn;
        }

        #endregion

        #region Targets

        private static void SetOrigin(Piece piece)
        {
            if (piece.IsEmpty) return;
            if (piece.Side != CurrentPlayerTurn) {
                Debug.Log("You can't play an Opponent piece !");
                return;
            }
                    
            _origin = piece;
            _moves = Matrix.GetMoves(_origin);

            GetPieceBehaviour(_origin).Highlight(_moves.Count > 0 ? HighlightType.Active : HighlightType.Error);

            Board.EnableCellsTargets(_moves);
        }

        private static void SetDestination(Piece piece)
        {
            if (!_moves.Contains(piece)) return;
                    
            _destination = piece;
            // ResolveMovement();
        }

        #endregion

        #region Checks handling

        /// <summary>
        /// Verify if the player from a specific side is in check from the supplied "snapshot" grid
        /// </summary>
        /// <param name="grid">The original grid or the duplicate to check</param>
        /// <param name="sideToTest">Side of the player to verify</param>
        /// <returns></returns>
        private static bool PlayerIsInCheck(Piece[,] grid, Side sideToTest)
        {
            Piece king = Matrix.GetKing(grid, sideToTest);
            List<Piece> opponentPieces = Matrix.GetPiecesFromSide(grid, sideToTest == Side.Light ? Side.Dark : Side.Light);

            ClearChecks();

            foreach (Piece opponentCell in opponentPieces) // For each opponent pieces
            {
                List<Piece> possibleMoves = opponentCell.AvailableMoves(opponentCell.Coordinates);

                foreach (Piece target in possibleMoves) // For each possibility
                {
                    if (target is { IsEmpty: false } && target.Equals(king)) // If one of them threaten the King
                    {
                        Debug.Log($"{opponentCell.Side} {opponentCell.GetType().Name} make the {king.Side} {king.GetType().Name} in check, tracking down...");
                        CheckResponsibles.Add(opponentCell); // Track the piece
                        break;
                    }
                }

                if (CheckResponsibles.Count > 0) // If any opponent piece threaten the King
                {
                    Debug.Log($"{CheckResponsibles.Count} threats have been detected towards {king.Side} {king.GetType().Name}");
                    foreach (Piece responsible in CheckResponsibles) // For each King's threats
                    {
                        foreach (Piece piece in responsible.AvailableMoves(responsible.Coordinates)) {
                            GetPieceBehaviour(piece).Highlight(HighlightType.Error); // Highlight the threat line(s) 
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        private static void ClearChecks()
        {
            foreach (Piece cell in CheckResponsibles)
            {
                GetPieceBehaviour(cell).Highlight(HighlightType.None);
            }
                
            CheckResponsibles.Clear();
        }

        private static void GatherValidMoves()
        {
            Piece[,] snapshot = Matrix.GetCurrentGridSnapshot();

            foreach (Piece piece in snapshot) // For each piece from current player' side
            {
                if (piece.IsEmpty || piece.Side != CurrentPlayerTurn) continue;
                
                foreach (Piece move in piece.AvailableMoves(piece.Coordinates)) // For each moves this piece can achieve
                {
                    // Simulate a new grid (from the snapshot) by manually move the piece according to each of it's positions
                    VirtualResolve(snapshot, piece, move);
                }
                // Iterate next piece
            }
        }

        private static void VirtualResolve(Piece[,] snapshot, Piece origin, Piece destination) // Resolve every possible movements from a snapshot
        {
            Piece[,] virtualGrid = Matrix.DuplicateSnapshot(snapshot); // Preserve state for each piece' simulation
            Piece virtualOrigin = virtualGrid[origin.Coordinates.Row, origin.Coordinates.Column];
            Piece virtualDestination = virtualGrid[destination.Coordinates.Row, destination.Coordinates.Column];
                
            virtualDestination = virtualOrigin;
            virtualOrigin = null;

            if (!PlayerIsInCheck(virtualGrid, OpponentTurn))
            {
                if (ValidCheckMoves.TryGetValue(virtualDestination, out List<Piece> move))
                    move.Add(virtualDestination);
                else
                    ValidCheckMoves.Add(virtualDestination, new List<Piece> { virtualDestination });
            }
        }

        #endregion

        #region Utils

        private static bool IsDifferentPieceSelected(Piece piece) {
            return piece.IsEmpty || piece.Side == CurrentPlayerTurn;
        }

        public static void ShowNoMovesAvailable() {
            GetPieceBehaviour(_origin).Highlight(HighlightType.Error);
        }

        #endregion
    }
}
