using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    public class Pawn : Piece
    {
        public Pawn(Cell cell, GameObject prefab, Transform root, Side side) : base(cell, prefab, root, side) {}

        public override List<Cell> GetAvailableMoves(Cell currentCell)
        {
            List<Cell> moves = new List<Cell>();
            int currentColumn = currentCell.Coordinates.Columns;
            int currentRow = currentCell.Coordinates.Row;
            
            // First: Get forward cell
            
            //      // if (forward cell isn't occupied), add to the list
            
            //      // if (check if adjacentCells is occupied by Opponent), add them to list in this case.
            
            //      // if (!piece.HasMoved), check if (cell forward to the initial fowardCell isn't occupied), add the to the list; 
            
            return null;
        }
    }
}
