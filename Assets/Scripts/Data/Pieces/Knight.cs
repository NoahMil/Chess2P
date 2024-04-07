using System.Collections.Generic;
using Enums;

namespace Data.Pieces
{
    public class Knight : Piece
    {
        public Knight(Side side, Coordinates coords) : base(side, coords) {}
        
        public Knight(Knight copy) : base(copy) {}
        
        public override float Heuristic
        {
            get
            {
                float baseValue = 3.20f;
                return baseValue;
            }
        }

        public override List<Piece> AvailableMoves(Coordinates coordinates)
        {
            List<Piece> availableMoves = new List<Piece>();
            int currentColumn = coordinates.Column;
            int currentRow = coordinates.Row;

            int[] columnsOffsets = { 1, 2,  2,  1, -1, -2, -2, -1 };
            int[] rowOffsets     = { 2, 1, -1, -2, -2, -1,  1,  2 };

            for (int i = 0; i < Matrix.BoardSize; i++)
            {
                int column = currentColumn + columnsOffsets[i];
                int row = currentRow + rowOffsets[i];

                Piece piece = Matrix.GetPiece(column, row);
                
                ValidateCell(availableMoves, piece);
            }

            return availableMoves;
        }

        protected override bool ValidateCell(ICollection<Piece> availableMoves, Piece piece)
        {
            if (piece == null) return false; // Knight will check out-of-board cells, skipping them

            if (piece.IsEmpty || piece.Side != Side)
            {
                availableMoves.Add(piece);
                return true;
            }

            return false;
        }
    }
}
