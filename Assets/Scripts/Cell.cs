using System;
using UnityEngine;
using Pieces;
using Object = UnityEngine.Object;

public class Cell: IEquatable<Cell>
{
    public Coordinates Coordinates { get; }
    public Piece Occupant { get; set; }

    public bool IsOccupied => Occupant != null;

    public Cell(GameObject prefab, Transform root, int column, int row)
    {
        Coordinates = new Coordinates(column, row);
        GameObject cell = Object.Instantiate(prefab, Coordinates.World, Quaternion.identity, root);

        cell.name = (char)('A' + column) + (row + 1).ToString();
    }

    public Cell(Cell copy)
    {
        Coordinates = copy.Coordinates;
        Occupant = copy.Occupant;
    }

    public bool Equals(Cell other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Coordinates.Equals(other.Coordinates) && Equals(Occupant, other.Occupant);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Cell)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Coordinates, Occupant);
    }
}
