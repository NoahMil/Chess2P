using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static Side CurrentPlayerTurn = Side.Light;
        public static bool Checkmate = false;
        
        private Side NextTurn => (CurrentPlayerTurn == Side.Light ? Side.Dark : Side.Light);
        
        private bool _showedOnce;
    }
}
