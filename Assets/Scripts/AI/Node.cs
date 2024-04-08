using System.Collections.Generic;

using Data;
using Enums;

namespace AI
{
    public class Node
    {
        private Piece[,] Grid { get; set; }
        private Side Turn { get; set; }
        
        public Coordinates Piece { get; private set; }
        public Coordinates Destination { get; private set; }
        
        public Node(Side turn, Piece[,] grid, Coordinates origin, Coordinates destination)
        {
            Turn = turn;
            Grid = grid;
            Piece = origin;
            Destination = destination;
        }

        public List<Node> ConstructChilds()
        {
            List<Node> childs = new();
            List<Piece> allPieces = Matrix.GetPiece().
            return childs;
        }
    }
}
