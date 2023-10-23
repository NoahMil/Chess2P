using System;
using UnityEngine;

public class CellBehaviour: MonoBehaviour
{
    private Cell _internalCell;
    
    private void OnMouseDown()
    {
    }

    public void SetInternalCell(Cell cell)
    {
        _internalCell = cell;
    }
}
