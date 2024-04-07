using System.Collections.Generic;
using Enums;

namespace Data.Pieces
{
    public class Rook : Piece
    {
        public Rook(Side side, Coordinates coords) : base(side, coords) {}
        
        public Rook(Rook copy) : base(copy) {}
        
        public override float Heuristic
        {
            get
            {
                float baseValue = 5.10f;
                return baseValue;
            }
        }

        public override List<Piece> AvailableMoves(Coordinates coordinates)
        {
            List<Piece> availableMoves = new List<Piece>();
            int currentColumn = coordinates.Column;
            int currentRow = coordinates.Row;

            for (int row = currentRow + 1; row < Matrix.BoardSize; row++) // Upward
            {
                Piece upwardCell = Matrix.GetPiece(currentColumn, row);
                if (!ValidateCell(availableMoves, upwardCell))
                    break;
            }
            
            for (int row = currentRow - 1; row >= 0; row--) // Downward
            {
                Piece downwardCell = Matrix.GetPiece(currentColumn, row);
                if (!ValidateCell(availableMoves, downwardCell))
                    break;
            }
            
            for (int column = currentColumn + 1; column < Matrix.BoardSize; column++) // Rightward
            {
                Piece rightwardCell = Matrix.GetPiece(column, currentRow);
                if (!ValidateCell(availableMoves, rightwardCell))
                    break;
            }

            for (int column = currentColumn - 1; column >= 0; column--) // Leftward
            {
                Piece leftwardCell = Matrix.GetPiece(column, currentRow);
                if (!ValidateCell(availableMoves, leftwardCell))
                    break;
            }

            return availableMoves;
        }
    }
}
