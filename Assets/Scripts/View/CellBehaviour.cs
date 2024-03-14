using System;
using Managers;
using UnityEngine;

namespace View
{
    public class CellBehaviour: MonoBehaviour
    {
        public Coordinates Coordinates;
        
        private MeshRenderer _mesh;
        private Collider _collider;
    
        private static readonly int Enable = Shader.PropertyToID("_Enable");
        private static readonly int IntersectionColor = Shader.PropertyToID("_IntersectionColor");
        private static readonly int IntersectionColorAlt = Shader.PropertyToID("_IntersectionColorAlt");
        
        private static Color _initialColor;

        private void Awake()
        {
            Vector3 position = transform.position;
            Coordinates = new Coordinates(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
            
            _mesh = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
            _initialColor = _mesh.material.GetColor(IntersectionColor);
        }

        private void OnMouseDown()
        {
            GameManager.SelectCell(Matrix.GetCell(gameObject.name));
        }

        public void IsTargetable(bool enable)
        {
            _collider.enabled = enable;
        }

        public void Highlight(bool enable)
        {
            int value = Convert.ToInt32(enable);
            _mesh.material.SetColor(IntersectionColor, _initialColor);
            _mesh.material.SetFloat(Enable, value);
        }

        public void HighlightCheck(bool enable)
        {
            int value = Convert.ToInt32(enable);
            Color altColor = _mesh.material.GetColor(IntersectionColorAlt);
            
            _mesh.material.SetColor(IntersectionColor, altColor);
            _mesh.material.SetFloat(Enable, value);
        }
    }
}
