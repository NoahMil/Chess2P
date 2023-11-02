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
            Cell forwardLeft = Matrix.GetCell(currentColumn - 1, currentRow + offset);
            Cell forwardRight = Matrix.GetCell(currentColumn + 1, currentRow + offset);
            Cell forwardPush = Matrix.GetCell(currentColumn, currentRow + offset * 2);

            if (forward is { IsOccupied: false })
                moves.Add(forward);

            if (forwardLeft is { IsOccupied: true } && forwardLeft.Occupant.Side != this.Side)
                moves.Add(forwardLeft);

            if (forwardRight is { IsOccupied: true } && forwardRight.Occupant.Side != this.Side)
                moves.Add(forwardRight);
            
            if (forwardPush != null && !HasMoved && !forwardPush.IsOccupied)
                moves.Add(forwardPush);
            
            return moves;
        }
    }
}
