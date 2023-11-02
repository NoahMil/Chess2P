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

            GetColumnCells(currentColumn, currentRow, moves);
            GetRowCells(currentColumn, currentRow, moves);

            return moves;
        }

        private void GetColumnCells(int currentColumn, int currentRow, ICollection<Cell> availablesMoves)
        {
            for (int i = currentRow + 1; i < Matrix.BoardSize; i++) // Upward
            {
                Cell upwardCell = Matrix.GetCell(currentColumn, i);
                if (!ValidateCell(availablesMoves, upwardCell))
                    break;
            }
            
            for (int i = currentRow - 1; i >= 0; i--) // Downward
            {
                Cell downwardCell = Matrix.GetCell(currentColumn, i);
                if (!ValidateCell(availablesMoves, downwardCell))
                    break;
            }
        }
        
        private void GetRowCells(int currentColumn, int currentRow, ICollection<Cell> availablesMoves)
        {
            for (int i = currentColumn + 1; i < Matrix.BoardSize; i++) // Rightward
            {
                Cell rightwardCell = Matrix.GetCell(i, currentRow);
                if (!ValidateCell(availablesMoves, rightwardCell))
                    break;
            }

            for (int i = currentColumn - 1; i >= 0; i--) // Leftward
            {
                Cell leftwardCell = Matrix.GetCell(i, currentRow);
                if (!ValidateCell(availablesMoves, leftwardCell))
                    break;
            }
        }

        private bool ValidateCell(ICollection<Cell> availableMoves, Cell cell)
        {
            if (cell.IsOccupied)
            {
                if (cell.Occupant.Side != Side) // Found a Opponent to take out
                    availableMoves.Add(cell);

                return false;
            }
            
            availableMoves.Add(cell);
            return true;
        }
    }
}
