﻿using System.Collections.Generic;
using Data;
using Enums;

namespace Pieces
{
    public class Bishop : Piece
    {
        public override int HeuristicScore => 3;

        public Bishop(Cell cell, Side side) : base(cell, side) {}

        public override List<Cell> AvailableMoves()
        {
            List<Cell> availableMoves = new List<Cell>();
            int currentColumn = this.Cell.Coordinates.Column;
            int currentRow = this.Cell.Coordinates.Row;
            
            for (int column = currentColumn + 1, row = currentRow + 1; row < Matrix.BoardSize && column < Matrix.BoardSize; row++, column++) // Upward-right
            {
                Cell cell = Matrix.GetCell(column, row);
                if (!ValidateCell(availableMoves, cell))
                    break;
            }
            
            for (int column = currentColumn - 1, row = currentRow + 1; row < Matrix.BoardSize && column >= 0; row++, column--) // Upward-left
            {
                Cell cell = Matrix.GetCell(column, row);
                if (!ValidateCell(availableMoves, cell))
                    break;
            }

            for (int column = currentColumn + 1, row = currentRow - 1; row >= 0 && column < Matrix.BoardSize; row--, column++) // Downward-right
            {
                Cell cell = Matrix.GetCell(column, row);
                if (!ValidateCell(availableMoves, cell))
                    break;
            }

            for (int column = currentColumn - 1, row = currentRow - 1; row >= 0 && column >= 0; row--, column--) // Downward-left
            {
                Cell cell = Matrix.GetCell(column, row);
                if (!ValidateCell(availableMoves, cell))
                    break;
            }

            return availableMoves;
        }
    }
}
