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
            
            if (forwardLeft is { IsOccupied: true } && forwardLeft.Occupant.Side != Side)
                moves.Add(forwardLeft);

            if (forwardRight is { IsOccupied: true } && forwardRight.Occupant.Side != Side)
                moves.Add(forwardRight);

            if (forward is { IsOccupied: false })
                moves.Add(forward);
            else
                return moves;
            
            if (forwardPush != null && !HasMoved && !forwardPush.IsOccupied)
                moves.Add(forwardPush);
            
            return moves;
        }

        public override List<Cell> GetPathToKing(Cell currentCell)
        {
            List<Cell> moves = GetAvailableMoves(currentCell);
            List<Cell> pathToKing = new();

            foreach (Cell cell in moves)
            {
                if (cell.IsOccupied && cell.Occupant.IsTheKing && cell.Occupant.Side != Side)
                {
                    pathToKing.Add(cell);
                }
            }
            
            return pathToKing;
        }
    }
}
