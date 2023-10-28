using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static Side CurrentPlayerTurn { get; set; }
        public static bool Checkmate { get; set; }
        
        private static Side OpponentTurn => (CurrentPlayerTurn == Side.Light ? Side.Dark : Side.Light);

        private static Cell _origin;
        private static Cell _destination;
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
            if (_destination.IsOccupied && _destination.Occupant.Side == OpponentTurn)
                Destroy(_destination.Occupant.Behaviour.gameObject);

            _destination.Occupant = _origin.Occupant;
            _origin.Occupant = null;
            
            Board.UpdateView();
            CheckEndOfGame();
            ChangeTurn();
        }

        private static void ChangeTurn()
        {
            CurrentPlayerTurn = OpponentTurn;
            _showedOnce = false;
            _destination = null;
            _origin = null;
        }

        private static void CheckEndOfGame()
        {
            
        }

        public static void SelectCell(Cell cell)
        {
            if (_origin == null)
            {
                if (!cell.IsOccupied) return;
                if (cell.Occupant.Side != CurrentPlayerTurn) {
                    Debug.Log("You can't play an Opponent piece !");
                    return;
                }
                _origin = cell;
                _origin.Occupant.Behaviour.Highlight(true);
                
                Debug.Log("Is Pawn has moved");
                Board.EnableCellsTargets(Matrix.GetMoves(_origin));
            }
            else
            {
                _destination = cell;
                ResolveMovement();
            }
        }

        public static void Unselect()
        {
            if (_origin == null) return;
            
            _origin.Occupant.Behaviour.Highlight(false);
            _origin = null;
        }
    }
}
