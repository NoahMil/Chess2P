using UnityEngine;

namespace Pieces
{
    public class Queen : Piece
    {
        public Queen(GameObject prefab, Transform root, Coordinates pos, Side side) : base(prefab, root, pos, side) {}
        
        public override void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}
