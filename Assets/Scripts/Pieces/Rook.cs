using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Pieces
{
    public class Rook : Piece
    {
        public Rook(Cell cell, GameObject prefab, Transform root, Side side) : base(cell, prefab, root, side) {}

        public override List<Cell> GetAvailableMoves(Cell currentCell)
        {
            List<Cell> availableMoves = new List<Cell>();
            int currentColumn = currentCell.Coordinates.Columns;
            int currentRow = currentCell.Coordinates.Row;

            for (int row = currentRow + 1; row < Matrix.BoardSize; row++) // Upward
            {
                Cell upwardCell = Matrix.GetCell(currentColumn, row);
                if (!ValidateCell(availableMoves, upwardCell))
                    break;
            }
            
            for (int row = currentRow - 1; row >= 0; row--) // Downward
            {
                Cell downwardCell = Matrix.GetCell(currentColumn, row);
                if (!ValidateCell(availableMoves, downwardCell))
                    break;
            }
            
            for (int column = currentColumn + 1; column < Matrix.BoardSize; column++) // Rightward
            {
                Cell rightwardCell = Matrix.GetCell(column, currentRow);
                if (!ValidateCell(availableMoves, rightwardCell))
                    break;
            }

            for (int column = currentColumn - 1; column >= 0; column--) // Leftward
            {
                Cell leftwardCell = Matrix.GetCell(column, currentRow);
                if (!ValidateCell(availableMoves, leftwardCell))
                    break;
            }

            return availableMoves;
        }

        public override List<Cell> GetPathToKing(Cell currentCell)
        {
            List<Cell> pathToKing = new List<Cell>();
            int currentColumn = currentCell.Coordinates.Columns;
            int currentRow = currentCell.Coordinates.Row;

            for (int row = currentRow + 1; row < Matrix.BoardSize; row++) // Upward
            {
                Cell upwardCell = Matrix.GetCell(currentColumn, row);
                if (!ValidateCell(pathToKing, upwardCell))
                    if (pathToKing.Contains(Matrix.GetKing(GameManager.OpponentTurn)))
                        return pathToKing;
                    else
                        pathToKing.Clear();
            }
            
            for (int row = currentRow - 1; row >= 0; row--) // Downward
            {
                Cell downwardCell = Matrix.GetCell(currentColumn, row);
                if (!ValidateCell(pathToKing, downwardCell))
                    if (pathToKing.Contains(Matrix.GetKing(GameManager.OpponentTurn)))
                        return pathToKing;
                    else
                        pathToKing.Clear();
            }
            
            for (int column = currentColumn + 1; column < Matrix.BoardSize; column++) // Rightward
            {
                Cell rightwardCell = Matrix.GetCell(column, currentRow);
                if (!ValidateCell(pathToKing, rightwardCell))
                    if (pathToKing.Contains(Matrix.GetKing(GameManager.OpponentTurn)))
                        return pathToKing;
                    else
                        pathToKing.Clear();
            }

            for (int column = currentColumn - 1; column >= 0; column--) // Leftward
            {
                Cell leftwardCell = Matrix.GetCell(column, currentRow);
                if (!ValidateCell(pathToKing, leftwardCell))
                    if (pathToKing.Contains(Matrix.GetKing(GameManager.OpponentTurn)))
                        return pathToKing;
                    else
                        pathToKing.Clear();
            }

            return null;
        }
    }
}
