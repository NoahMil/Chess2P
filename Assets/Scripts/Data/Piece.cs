using System;
using System.Collections.Generic;

using Managers;
using Enums;
using Data.Pieces;

namespace Data
{
    public abstract class Piece : IEquatable<Piece>, ICloneable
    {
        public Coordinates Coordinates { get; set; }
        public Side Side { get; set; }
        public bool HasMoved { get; set; }
        public string Name => GameManager.GetPieceBehaviour(this).Name;

        public abstract float Heuristic { get; }

        public bool IsEmpty => Side == Side.Empty;
        public bool IsTheKing => this.GetType() == typeof(King) || !IsEmpty;
        public bool IsNotTheKing => this.GetType() != typeof(King) || !IsEmpty;
        
        protected Piece(Side side, Coordinates coordinates)
        {
            Coordinates = coordinates;
            Side = side;
            HasMoved = false;
        }

        protected Piece(Piece copy)
        {
            this.Side = copy.Side;
            this.HasMoved = copy.HasMoved;
        }

        public static Piece Create(string prefabName, Coordinates coordinates)
        {
            return prefabName switch
            {
                "LightPawn"   => new Pawn   (Side.Light, coordinates),
                "LightRook"   => new Rook   (Side.Light, coordinates),
                "LightKnight" => new Knight (Side.Light, coordinates),
                "LightBishop" => new Bishop (Side.Light, coordinates),
                "LightQueen"  => new Queen  (Side.Light, coordinates),
                "LightKing"   => new King   (Side.Light, coordinates),
                "DarkPawn"    => new Pawn   (Side.Dark, coordinates),
                "DarkRook"    => new Rook   (Side.Dark, coordinates),
                "DarkKnight"  => new Knight (Side.Dark, coordinates),
                "DarkBishop"  => new Bishop (Side.Dark, coordinates),
                "DarkQueen"   => new Queen  (Side.Dark, coordinates),
                "DarkKing"    => new King   (Side.Dark, coordinates),
                
                _ => throw new ArgumentOutOfRangeException(prefabName, "Invalid piece name provided for Creation")
            };
        }
        
        public abstract List<Piece> AvailableMoves(Coordinates coords);
        
        protected virtual bool ValidateCell(ICollection<Piece> availableMoves, Piece piece)
        {
            if (piece is null) throw new IndexOutOfRangeException($"{GetType().Name} shouldn't try to validate any cell out of board !");

            if (!piece.IsEmpty)
            {
                if (piece.Side != Side)
                    availableMoves.Add(piece);

                return false;
            }

            availableMoves.Add(piece);
            return true;
        }

        #region Equality and Copy

        public object Clone()
        {
            return MemberwiseClone();
        }

        public bool Equals(Piece other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Coordinates.Equals(other.Coordinates) && Side == other.Side && HasMoved == other.HasMoved && Heuristic.Equals(other.Heuristic);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Piece)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Heuristic);
        }

        #endregion

        public void Clear(Coordinates newCoords)
        {
            Coordinates = newCoords;
            Side = Side.Empty;
            HasMoved = false;
        }
    }
}
