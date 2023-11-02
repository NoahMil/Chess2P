using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    public class Rook : Piece
    {
        public Rook(Cell cell, GameObject prefab, Transform root, Side side) : base(cell, prefab, root, side) {}

        public override List<Cell> GetAvailableMoves(Cell currentCell)
        {
            List<Cell> moves = new List<Cell>();
            int currentColumn = currentCell.Coordinates.Columns;
            int currentRow = currentCell.Coordinates.Row;
            
            return moves;
        }

        /* private List<Cell> AvailableRowCells(int currentRow)
        {
            int offset = (Side == Side.Light) ? 1 : -1;
            
            // if (Side)
        } */
        
        /*private List<Cell> AvailableColumunCells(int currentRow)
        {
            
        }*/
    }
}
