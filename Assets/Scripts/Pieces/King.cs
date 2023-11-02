using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    public class King : Piece
    {
        public King(Cell cell, GameObject prefab, Transform root, Side side) : base(cell, prefab, root, side) {}

        public override List<Cell> GetAvailableMoves(Cell currentCoordinates)
        {
            throw new System.NotImplementedException();
        }

        protected override bool ValidateCell(ICollection<Cell> availableMoves, Cell cell)
        {
            throw new System.NotImplementedException();
        }
    }
}
