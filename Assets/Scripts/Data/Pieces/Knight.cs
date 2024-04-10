using System.Collections.Generic;
using Enums;

namespace Data.Pieces
{
    public class Knight : Piece
    {
        public Knight(Side side, Coordinates coords, Piece[,] reference) : base(side, coords, reference) {}
        
        public Knight(Knight copy, Piece[,] reference) : base(copy, reference) {}
        
        public override float Heuristic
        {
            get
            {
                float baseValue = 3.20f;
                return baseValue;
            }
        }

        public override List<Coordinates> AvailableMoves()
        {
            List<Coordinates> availableMoves = new ();
            int currentColumn = this.Coordinates.Column;
            int currentRow = this.Coordinates.Row;

            int[] columnsOffsets = { 1, 2,  2,  1, -1, -2, -2, -1 };
            int[] rowOffsets     = { 2, 1, -1, -2, -2, -1,  1,  2 };

            for (int i = 0; i < Matrix.BoardSize; i++)
            {
                int column = currentColumn + columnsOffsets[i];
                int row = currentRow + rowOffsets[i];
                
                availableMoves.Add(new Coordinates(column, row));
            }

            this.ValidateMoves(ref availableMoves);
            return availableMoves;
        }

        protected override bool ValidateMoves(ref List<Coordinates> availableMoves)
        {
            List<Coordinates> trollList = new List<Coordinates>();

            foreach (Coordinates move in availableMoves)
            {
                if (move.Column is >= 0 and <= 7 && move.Row is >= 0 and <= 7)
                {
                    Piece destination = Matrix.GetPiece(Reference, move);
                    if (destination == null || (destination.Side != Side))
                    {
                        trollList.Add(move);
                        return true;
                    }
                }
            }
            availableMoves = trollList;
            return false;
        }
    }

}
