using System;
using Managers;
using UnityEngine;

public class CellBehaviour: MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.SelectCell(Matrix.GetCell(gameObject.name));
    }
}
