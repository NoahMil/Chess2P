using UnityEngine;

namespace WIP
{
    public class Position
    {
        public Vector2Int Coordinates { get; }
        public Piece Occupant { get; }

        public bool IsOccupied => Occupant != null;

        public Position(int x, int y, Piece initial = null)
        {
            Coordinates = new Vector2Int(x, y);
            Occupant = initial;
        }
    }
}
