using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Data.Pieces
{
    public class King : Piece
    {
        public King(Side side, Coordinates coords) : base(side, coords) {}

        public King(King copy) : base(copy) {}

        public override float Heuristic
        {
            get
            {
                float baseValue = Mathf.Infinity;
                return baseValue;
            }
        }

        public override List<Piece> AvailableMoves(Coordinates coordinates)
        {
            List<Piece> availableMoves = new List<Piece>();
            int currentColumn = coordinates.Column;
            int currentRow = coordinates.Row;

            int[] rowOffsets = { -1, -1, -1,  0, 0,  1, 1, 1 };
            int[] colOffsets = { -1,  0,  1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < Matrix.BoardSize; i++)
            {
                int row = currentRow + rowOffsets[i];
                int column = currentColumn + colOffsets[i];

                Piece piece = Matrix.GetPiece(column, row);
                ValidateCell(availableMoves, piece);
            }

            return availableMoves;
        }

        protected override bool ValidateCell(ICollection<Piece> availableMoves, Piece piece)
        {
            if (piece is null) return false; // King will check out-of-board cells, skipping them

            if (piece.IsEmpty || (piece.Side != Side && piece.IsNotTheKing))
            {
                availableMoves.Add(piece);
                return true;
            }

            return false;
        }
    }
}
