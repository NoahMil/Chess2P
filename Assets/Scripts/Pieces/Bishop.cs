using UnityEngine;

namespace Pieces
{
    public class Bishop : Piece
    {
        public Bishop(GameObject prefab, Transform root, Coordinates pos, Side side) : base(prefab, root, pos, side) {}
        
        public override void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}
