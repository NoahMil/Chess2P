using System;
using UnityEngine;
using SO;

public class Piece: MonoBehaviour
{
    [SerializeField] private PieceSO _blueprintSO;
    [SerializeField] private Side _side;
    
    public Cell CurrentCell { get; set; }

    private void OnMouseDown()
    {
        Debug.Log($"Selected: {_side} {_blueprintSO.name}");
    }
}
