using UnityEngine;

namespace Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Cell cell, GameObject prefab, Transform root, Side side) : base(cell, prefab, root, side) {}
        
        public override void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}
