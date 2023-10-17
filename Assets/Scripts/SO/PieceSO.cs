using UnityEngine;

namespace SO
{
    public abstract class PieceSO : ScriptableObject
    {
        public void Move(Piece reference, Cell destination)
        {
            if (destination.IsOccupied)
                Destroy(destination.Occupant.gameObject);
                
            reference.CurrentCell = destination;
            reference.CurrentCell.Occupant = reference;
            reference.UpdatePosition();
        }
    }
}
