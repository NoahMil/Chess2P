using UnityEngine;

namespace WIP
{
    public class Position
    {
        public Vector2Int Coordinates { get; }
        public BoardPiece Occupant { get; }

        public bool IsOccupied => Occupant != null;

        public Position(int x, int y, BoardPiece initial = null)
        {
            Coordinates = new Vector2Int(x, y);
            Occupant = initial;
        }
    }
}
