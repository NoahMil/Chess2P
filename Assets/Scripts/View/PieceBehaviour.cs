using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

using Managers;
using Data;
using Data.Pieces;
using Enums;

namespace View
{
    public class PieceBehaviour: MonoBehaviour
    {
        public CellBehaviour Cell { get; set; }
        public Coordinates Coordinates { get; set; }
        public string Name => gameObject.name;
        
        private MeshRenderer _mesh;

        private static readonly int FresnelColor = Shader.PropertyToID("_fresnelColor");
        private static readonly int EnableFresnel = Shader.PropertyToID("_enableFresnel");
        private static readonly int FresnelAltColor = Shader.PropertyToID("_fresnelAltColor");

        private static Color _initialColor;

        private void Awake()
        {
            Vector3 position = transform.position;
            Coordinates = new Coordinates(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
            _mesh = GetComponent<MeshRenderer>();
            _initialColor = _mesh.material.GetColor(FresnelColor);
        }

        public static PieceBehaviour Create(Piece piece, GameObject prefab, Transform root)
        {
            Side side = piece.Side;
            Quaternion rotation = side == Side.Light ? Quaternion.identity : Quaternion.Euler(0, -180, 0);
            
            return Instantiate(prefab, piece.Coordinates.World, rotation, root).GetComponent<PieceBehaviour>();
        }

        public static List<PieceBehaviour> InitBoard(Transform root)
        {
            List<PieceBehaviour> pieceBehaviours = new ();
            string[] pieceOrder = { "Rook", "Knight", "Bishop", "Queen", "King", "Bishop", "Knight", "Rook" };

            for (int column = 0; column < Matrix.BoardSize; column++)
            {
                Create(Matrix.GetPiece(column, 0), Board.Prefabs["Light" + pieceOrder[column]], root);
                Create(Matrix.GetPiece(column, 1), Board.Prefabs["LightPawn"], root);
                Create(Matrix.GetPiece(column, 7), Board.Prefabs["Dark" + pieceOrder[column]], root);
                Create(Matrix.GetPiece(column, 6), Board.Prefabs["DarkPawn"], root);

                pieceBehaviours.Add(GameManager.GetPieceBehaviour(Matrix.GetPiece(column, 0)));
                pieceBehaviours.Add(GameManager.GetPieceBehaviour(Matrix.GetPiece(column, 1)));
                pieceBehaviours.Add(GameManager.GetPieceBehaviour(Matrix.GetPiece(column, 7)));
                pieceBehaviours.Add(GameManager.GetPieceBehaviour(Matrix.GetPiece(column, 6)));
            }

            return pieceBehaviours;
        }

        public void Highlight(HighlightType type)
        {
            switch (type)
            {
                case HighlightType.None:
                    _mesh.material.SetFloat(EnableFresnel, 0);
                    break;
                case HighlightType.Active:
                    _mesh.material.SetColor(FresnelColor, _initialColor);
                    _mesh.material.SetFloat(EnableFresnel, 1);
                    break;
                case HighlightType.Error:
                    Color altColor = _mesh.material.GetColor(FresnelAltColor);
                    _mesh.material.SetColor(FresnelColor, altColor);
                    _mesh.material.SetFloat(EnableFresnel, 1);
                    break;
                default:
                    throw new InvalidEnumArgumentException($"Invalid {type.ToString()} supplied for piece highlighting");
            }
        }
    }
}
