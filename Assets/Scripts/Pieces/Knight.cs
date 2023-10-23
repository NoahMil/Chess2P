using UnityEngine;

namespace Pieces
{
    public class Knight : Piece
    {
        public Knight(GameObject prefab, Transform root, Coordinates pos, Side side) : base(prefab, root, pos, side) {}
        
        public override void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}
