using System;
using Data;
using UnityEngine;

namespace View
{
    public class Unit: MonoBehaviour
    {
        public Coordinates Coordinates { get; set; }
        
        [HideInInspector] public PieceBehaviour Piece;
        [HideInInspector] public CellBehaviour Cell;
        
        public bool IsEmpty => Piece == null;
    }
}
