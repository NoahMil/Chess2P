using UnityEngine;

namespace Pieces
{
    public class King : Piece
    {
        public King(GameObject prefab, Transform root, Coordinates pos, Side side) : base(prefab, root, pos, side) {}
        
        public override void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}
