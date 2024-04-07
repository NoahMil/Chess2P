using System.ComponentModel;
using UnityEngine;

using Data;
using Enums;

namespace View
{
    public class PieceBehaviour: MonoBehaviour
    {
        private static readonly int FresnelColor = Shader.PropertyToID("_fresnelColor");
        private static readonly int EnableFresnel = Shader.PropertyToID("_enableFresnel");
        private static readonly int FresnelAltColor = Shader.PropertyToID("_fresnelAltColor");
        private static Color _initialColor;

        private Unit _parent;
        private MeshRenderer _mesh;
        
        private void Awake()
        {
            _parent = transform.parent.GetComponent<Unit>();
            _mesh = GetComponent<MeshRenderer>();
            _initialColor = _mesh.material.GetColor(FresnelColor);
        }

        #region Fluff

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
                    throw new InvalidEnumArgumentException($"Invalid {type.ToString()} supplied for ${_parent.name}'s piece highlighting");
            }
        }

        #endregion
        
        #region Static

        public static PieceBehaviour Create(Unit parentObject, Piece matrixPiece)
        {
            if (matrixPiece == null || matrixPiece.IsEmpty) return null;

            Vector3 rotation = new (0, matrixPiece.Side == Side.Light ? 0 : 180, 0);
            
            string prefabType = matrixPiece.Side + matrixPiece.Type;
            GameObject prefab = Board.Prefabs[prefabType];
            GameObject piece = Instantiate(prefab, matrixPiece.Coordinates.World, Quaternion.Euler(rotation), parentObject.transform);
            
            piece.name = prefabType;
            
            return piece.GetComponent<PieceBehaviour>();
        }

        #endregion
    }
}
