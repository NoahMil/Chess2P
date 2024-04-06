using System.ComponentModel;
using UnityEngine;

using Enums;

namespace View
{
    public class CellBehaviour: MonoBehaviour
    {
        private Unit _parent;
        
        private MeshRenderer _mesh;
        private Collider _collider;
        
        private static readonly int Enable = Shader.PropertyToID("_Enable");
        private static readonly int IntersectionColor = Shader.PropertyToID("_IntersectionColor");
        private static readonly int IntersectionColorAlt = Shader.PropertyToID("_IntersectionColorAlt");
        private Color _initialColor;

        public bool IsTargetable {
            get => _collider.enabled;
            set => _collider.enabled = value;
        }

        private void Awake()
        {
            _parent = transform.parent.GetComponent<Unit>();
            _mesh = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
            _initialColor = _mesh.material.GetColor(IntersectionColor);

        }
        
        #region Fluff
        
        public void Highlight(HighlightType type)
        {
            switch (type)
            {
                case HighlightType.None:
                    _mesh.material.SetFloat(Enable, 0);
                    break;
                case HighlightType.Active:
                    _mesh.material.SetColor(IntersectionColor, _initialColor);
                    _mesh.material.SetFloat(Enable, 1);
                    break;
                case HighlightType.Error:
                    Color altColor = _mesh.material.GetColor(IntersectionColorAlt);
                    _mesh.material.SetColor(IntersectionColor, altColor);
                    _mesh.material.SetFloat(Enable, 1);
                    break;
                default:
                    throw new InvalidEnumArgumentException($"Invalid {type.ToString()} supplied for ${_parent.name}'s cell highlighting");
            }
        }
        
        #endregion

        #region Static

        public static CellBehaviour Create(Unit parentObject, GameObject prefab)
        {
            GameObject cell = Instantiate(prefab, parentObject.Coordinates.World, Quaternion.identity, parentObject.transform);
            
            cell.name = "Cell";
            
            return cell.GetComponent<CellBehaviour>();
        }

        #endregion
    }
}
