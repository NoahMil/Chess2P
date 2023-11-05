using System;
using UnityEngine;

using Managers;

namespace MonoBehaviours
{
    public class CellBehaviour: MonoBehaviour
    {
        private MeshRenderer _mesh;
        private Collider _collider;
    
        private static readonly int Enable = Shader.PropertyToID("_Enable");
        private static readonly int IntersectionColor = Shader.PropertyToID("_IntersectionColor");

        public static Color defaultColor;
        
        private void Awake()
        {
            _mesh = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
            defaultColor = _mesh.material.GetColor(IntersectionColor);
        }

        private void OnMouseDown()
        {
            GameManager.SelectCell(Matrix.GetCell(gameObject.name));
        }

        public void IsTargetable(bool enable)
        {
            _collider.enabled = enable;
        }

        public void Highlight(bool enable, Color color)
        {
            int value = Convert.ToInt32(enable);
            _mesh.material.SetColor(IntersectionColor, color);
            _mesh.material.SetFloat(Enable, value);
        }
    }
}
