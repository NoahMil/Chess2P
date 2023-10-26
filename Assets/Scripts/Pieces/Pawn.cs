using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Cell cell, GameObject prefab, Transform root, Side side) : base(cell, prefab, root, side) {}

        public override List<Cell> GetAvailableMoves(Cell currentCell)
        {
            List<Cell> moves = new List<Cell>();
            int currentColumn = currentCell.Coordinates.Columns;
            int currentRow = currentCell.Coordinates.Row;
            int offset = (Side == Side.Light) ? 1 : -1;
            
            Cell forward = Matrix.GetCell(currentColumn, currentRow + offset);
            Cell forwardPush = Matrix.GetCell(currentColumn, currentRow + offset + 1);
            Cell forwardLeft = Matrix.GetCell(currentColumn - 1, currentRow + offset);
            Cell forwardRight = Matrix.GetCell(currentColumn + 1, currentRow + offset);
            
            if (!forward.IsOccupied)
                moves.Add(forward);
            
            if (forwardLeft.IsOccupied && forwardLeft.Occupant.Side != Side)
                moves.Add(forwardLeft);
            
            if (forwardRight.IsOccupied && forwardRight.Occupant.Side != Side)
                moves.Add(forwardLeft);
            
            if (!HasMoved)
                moves.Add(forwardPush);
            
            return moves;
        }
    }
}
