using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pieces
{
    public abstract class Piece
    {
        public static Dictionary<string, GameObject> prefabs;
        private Side _side;
        
        protected Piece(GameObject prefab, Transform root, Coordinates pos, Side side)
        {
            Quaternion rotation = side == Side.Light ? Quaternion.identity : Quaternion.Euler(0, -180, 0);
            
            Object.Instantiate(prefab, pos.World, rotation, root);
        }

        public static Piece Create(string prefabName, Transform root, Coordinates coordinates)
        {
            return prefabName switch
            {
                "LightPawn"   => new Pawn   (prefabs["LightPawn"], root, coordinates, Side.Light),
                "LightRook"   => new Rook   (prefabs["LightRook"], root, coordinates, Side.Light),
                "LightKnight" => new Knight (prefabs["LightKnight"], root, coordinates, Side.Light),
                "LightBishop" => new Bishop (prefabs["LightBishop"], root, coordinates, Side.Light),
                "LightQueen"  => new Queen  (prefabs["LightQueen"], root, coordinates, Side.Light),
                "LightKing"   => new King   (prefabs["LightKing"], root, coordinates, Side.Light),
                "DarkPawn"    => new Pawn   (prefabs["DarkPawn"], root, coordinates, Side.Dark),
                "DarkRook"    => new Rook   (prefabs["DarkRook"], root, coordinates, Side.Dark),
                "DarkKnight"  => new Knight (prefabs["DarkKnight"], root, coordinates, Side.Dark),
                "DarkBishop"  => new Bishop (prefabs["DarkBishop"], root, coordinates, Side.Dark),
                "DarkQueen"   => new Queen  (prefabs["DarkQueen"], root, coordinates, Side.Dark),
                "DarkKing"    => new King   (prefabs["DarkKing"], root, coordinates, Side.Dark),
                
                _ => throw new ArgumentOutOfRangeException(prefabName, "Invalid piece name provided for Creation")
            };
        }
        
        public abstract void Move();
    }
}
