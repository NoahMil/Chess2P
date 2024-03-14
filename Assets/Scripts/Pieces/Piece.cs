using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

using MonoBehaviours;

namespace Pieces
{
    public abstract class Piece
    {
        public static Dictionary<string, GameObject> prefabs;
        
        public PieceBehaviour Behaviour { get; private set; }
        public Side Side { get; private set; }
        public bool HasMoved { get; set; }
        
        public bool IsTheKing => this.GetType() == typeof(King);
        public bool IsNotTheKing => this.GetType() != typeof(King);
        
        protected Piece(Cell cell, GameObject prefab, Transform root, Side side)
        {
            Quaternion rotation = side == Side.Light ? Quaternion.identity : Quaternion.Euler(0, -180, 0);
            GameObject piece = Object.Instantiate(prefab, cell.Coordinates.World, rotation, root);
            Behaviour = piece.GetComponent<PieceBehaviour>();
            Side = side;
            HasMoved = false;
        }
        
        protected abstract int Heuristic { get; }

        public static Piece Create(string prefabName, Cell originCell, Transform root)
        {
            return prefabName switch
            {
                "LightPawn"   => new Pawn   (originCell, prefabs["LightPawn"], root, Side.Light),
                "LightRook"   => new Rook   (originCell, prefabs["LightRook"], root, Side.Light),
                "LightKnight" => new Knight (originCell, prefabs["LightKnight"], root, Side.Light),
                "LightBishop" => new Bishop (originCell, prefabs["LightBishop"], root, Side.Light),
                "LightQueen"  => new Queen  (originCell, prefabs["LightQueen"], root, Side.Light),
                "LightKing"   => new King   (originCell, prefabs["LightKing"], root, Side.Light),
                "DarkPawn"    => new Pawn   (originCell, prefabs["DarkPawn"], root, Side.Dark),
                "DarkRook"    => new Rook   (originCell, prefabs["DarkRook"], root, Side.Dark),
                "DarkKnight"  => new Knight (originCell, prefabs["DarkKnight"], root, Side.Dark),
                "DarkBishop"  => new Bishop (originCell, prefabs["DarkBishop"], root, Side.Dark),
                "DarkQueen"   => new Queen  (originCell, prefabs["DarkQueen"], root, Side.Dark),
                "DarkKing"    => new King   (originCell, prefabs["DarkKing"], root, Side.Dark),
                
                _ => throw new ArgumentOutOfRangeException(prefabName, "Invalid piece name provided for Creation")
            };
        }
        
        public abstract List<Cell> GetAvailableMoves(Cell currentCell);
        
        protected virtual bool ValidateCell(ICollection<Cell> availableMoves, Cell cell)
        {
            if (cell is null) throw new IndexOutOfRangeException($"{GetType().Name} shouldn't try to validate any cell out of board !");

            if (cell.IsOccupied)
            {
                if (cell.Occupant.Side != Side)
                    availableMoves.Add(cell);

                return false;
            }

            availableMoves.Add(cell);
            return true;
        }
    }
}
