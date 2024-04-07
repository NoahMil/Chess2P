using System.Collections.Generic;
using Enums;

namespace Data.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Side side, Coordinates coords) : base(side, coords) {}

        public Bishop(Bishop copy) : base(copy) {}

        public override float Heuristic
        {
            get
            {
                float baseValue = 3.30f;
                return baseValue;
            }
        }

        public override List<Piece> AvailableMoves(Coordinates coordinates)
        {
            List<Piece> availableMoves = new List<Piece>();
            int currentColumn = coordinates.Column;
            int currentRow = coordinates.Row;
            
            for (int column = currentColumn + 1, row = currentRow + 1; row < Matrix.BoardSize && column < Matrix.BoardSize; row++, column++) // Upward-right
            {
                Piece piece = Matrix.GetPiece(column, row);
                if (!ValidateCell(availableMoves, piece))
                    break;
            }
            
            for (int column = currentColumn - 1, row = currentRow + 1; row < Matrix.BoardSize && column >= 0; row++, column--) // Upward-left
            {
                Piece piece = Matrix.GetPiece(column, row);
                if (!ValidateCell(availableMoves, piece))
                    break;
            }

            for (int column = currentColumn + 1, row = currentRow - 1; row >= 0 && column < Matrix.BoardSize; row--, column++) // Downward-right
            {
                Piece piece = Matrix.GetPiece(column, row);
                if (!ValidateCell(availableMoves, piece))
                    break;
            }

            for (int column = currentColumn - 1, row = currentRow - 1; row >= 0 && column >= 0; row--, column--) // Downward-left
            {
                Piece piece = Matrix.GetPiece(column, row);
                if (!ValidateCell(availableMoves, piece))
                    break;
            }

            return availableMoves;
        }
    }
}
