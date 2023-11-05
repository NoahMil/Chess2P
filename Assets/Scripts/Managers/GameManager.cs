using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using MonoBehaviours;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static Action<Side> onMoveMade;
        
        public static Side CurrentPlayerTurn { get; private set; }
        public static Side OpponentTurn => (CurrentPlayerTurn == Side.Light ? Side.Dark : Side.Light);
        public static bool Checkmate { get; set; }
        public static bool Check { get; set; }
        
        private static Cell _origin;
        private static Cell _destination;
        private static List<Cell> _moves;
        private static bool _showedOnce;

        private bool _confirmEscape;
        private float _escapeTimer = 3f;

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

            EmergencyEscape(Input.GetKeyDown(KeyCode.Escape));
        }

        private void EmergencyEscape(bool triggered)
        {
            if (triggered && _confirmEscape)
            {
                EditorApplication.ExitPlaymode();
                Application.Quit();
            }
            
            if (triggered) {
                _confirmEscape = true;
            }

            if (_escapeTimer <= 0f)
                _escapeTimer = 3f;

            _escapeTimer -= Time.deltaTime;
        }

        private static void ResolveMovement()
        {
            _origin.Occupant.Behaviour.Highlight(false);
            
            if (_destination is { IsOccupied: true } && _destination.Occupant.Side == OpponentTurn)
                Destroy(_destination.Occupant.Behaviour.gameObject);

            _destination.Occupant = _origin.Occupant;
            _destination.Occupant.HasMoved = true;
            _origin.Occupant = null;
            
            Board.UpdateView();
            CheckEndOfGame();
            ChangeTurn();
        }

        private static void ChangeTurn()
        {
            
            _showedOnce = false;
            _destination = null;
            _origin = null;

            foreach (Cell cell in _moves)
                cell.Behaviour.Highlight(false, CellBehaviour.defaultColor);
            _moves = null;
            
            onMoveMade?.Invoke(OpponentTurn); // Upon ending a turn, raise the event "MoveMade";
            CurrentPlayerTurn = OpponentTurn;
        }

        private static void CheckEndOfGame()
        {
            
        }

        public static void SelectCell(Cell cell)
        {
            if (Check)
            {
                if (cell != Matrix.GetKing(CurrentPlayerTurn))
                {
                    Debug.Log($"{CurrentPlayerTurn.ToString()} King is in Check");
                }
            }
            else
            {
                if (_origin == null)
                {
                    SetOrigin(cell);
                }
                else if (_origin != null && IsDifferentPieceSelected(cell)) // If the player wants to play another piece than the first selected
                {
                    Unselect();
                    // _origin = cell;
                    // SetOrigin(cell);
                    SelectCell(cell); // Tail Recursion
                }
                else
                {
                    SetDestination(cell);
                }
            }
        }

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

        private static bool IsDifferentPieceSelected(Cell cell)
        {
            return cell.IsOccupied && cell.Occupant.Side == CurrentPlayerTurn;
        }

        public static void ShowNoMovesAvailable()
        {
            _origin.Occupant.Behaviour.HighlightError(true);
        }

        private static void Unselect()
        {
            if (_origin == null) return;
            
            _origin.Occupant.Behaviour.Highlight(false);
            _origin.Occupant.Behaviour.HighlightError(false);
            _origin = null;

            if (_moves == null) return;

            foreach (Cell cell in _moves)
                cell.Behaviour.Highlight(false, CellBehaviour.defaultColor);
            _moves = null;
        }
    }
}
