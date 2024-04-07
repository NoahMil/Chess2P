using System.Collections.Generic;
using Enums;

namespace Data.Pieces
{
    public class Queen : Piece
    {
        public Queen(Side side, Coordinates coords) : base(side, coords) {}
        
        public Queen(Queen copy) : base(copy) {}
        
        public override float Heuristic
        {
            get
            {
                float baseValue = 8.80f;
                return baseValue;
            }
        }

        public override List<Piece> AvailableMoves(Coordinates coordinates)
        {
            List<Piece> availableMoves = new List<Piece>();
            int currentColumn = coordinates.Column;
            int currentRow = coordinates.Row;
            
            GetAlignedCells(availableMoves, currentColumn, currentRow);
            GetDiagonalCells(availableMoves, currentColumn, currentRow);
            
            return availableMoves;
        }
        
        private void GetDiagonalCells(ICollection<Piece> availableMoves, int currentColumn, int currentRow)
        {
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
        }
        
        private void GetAlignedCells(ICollection<Piece> availableMoves, int currentColumn, int currentRow)
        {
            for (int i = currentRow + 1; i < Matrix.BoardSize; i++) // Upward
            {
                Piece upwardCell = Matrix.GetPiece(currentColumn, i);
                if (!ValidateCell(availableMoves, upwardCell))
                    break;
            }
            
            for (int i = currentRow - 1; i >= 0; i--) // Downward
            {
                Piece downwardCell = Matrix.GetPiece(currentColumn, i);
                if (!ValidateCell(availableMoves, downwardCell))
                    break;
            }
            
            for (int i = currentColumn + 1; i < Matrix.BoardSize; i++) // Rightward
            {
                Piece rightwardCell = Matrix.GetPiece(i, currentRow);
                if (!ValidateCell(availableMoves, rightwardCell))
                    break;
            }

            for (int i = currentColumn - 1; i >= 0; i--) // Leftward
            {
                Piece leftwardCell = Matrix.GetPiece(i, currentRow);
                if (!ValidateCell(availableMoves, leftwardCell))
                    break;
            }
        }
    }
}
