using System.Collections.Generic;
using Enums;

namespace Data.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Side side, Coordinates coords) : base(side, coords) {}
        
        public Pawn(Pawn copy) : base(copy) {}

        public override float Heuristic
        {
            get
            {
                float baseValue = 1.0f;
                return baseValue;
            }
        }

        public override List<Piece> AvailableMoves(Coordinates coordinates)
        {
            List<Piece> moves = new List<Piece>();
            int currentColumn = coordinates.Column;
            int currentRow = coordinates.Row;
            int offset = (Side == Side.Light) ? 1 : -1;
            
            Piece forward = Matrix.GetPiece(currentColumn, currentRow + offset);
            Piece forwardLeft = Matrix.GetPiece(currentColumn - 1, currentRow + offset);
            Piece forwardRight = Matrix.GetPiece(currentColumn + 1, currentRow + offset);
            Piece forwardPush = Matrix.GetPiece(currentColumn, currentRow + offset * 2);
            
            if (forwardLeft is { IsEmpty: false } && forwardLeft.Side != Side)
                moves.Add(forwardLeft);

            if (forwardRight is { IsEmpty: false } && forwardRight.Side != Side)
                moves.Add(forwardRight);

            if (forward is { IsEmpty: true })
                moves.Add(forward);
            else
                return moves;
            
            if (forwardPush is { IsEmpty: true } && !HasMoved)
                moves.Add(forwardPush);
            
            return moves;
        }
    }
}
