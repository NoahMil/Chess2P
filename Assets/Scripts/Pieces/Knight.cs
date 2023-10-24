using UnityEngine;

namespace Pieces
{
    public class Knight : Piece
    {
        public Knight(Cell cell, GameObject prefab, Transform root, Side side) : base(cell, prefab, root, side) {}
        
        public override void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}
