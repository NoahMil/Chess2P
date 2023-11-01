using UnityEngine;

public struct Coordinates
{
    public int Row { get; set; }
    public int Columns { get; set; }

    public Vector3 World => new Vector3(Columns, 0.001f, Row);
    
    public Coordinates(int column, int row)
    {
        Row = row;
        Columns = column;
    }
}
