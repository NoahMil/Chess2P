using System.Collections.Generic;

using Data;
using Enums;

namespace IA
{
    public class Node
    {
        public Piece[,] Grid { get; private set; }
        
        public float Heuristic { get; private set; }
        
        public Node(Piece[,] snapshot, Side currentPlayerTurn)
        {
            Grid = snapshot;
        }

        public bool IsTerminal()
        {
            return false;
        }

        public List<Node> DiscoverChild()
        {
            List<Node> children = new();
            
            // List<Cell> moves = 
            return null;
        }
    }
}
