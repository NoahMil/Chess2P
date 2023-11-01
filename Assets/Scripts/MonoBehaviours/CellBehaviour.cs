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

        private void Awake()
        {
            _mesh = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
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
            _mesh.material.SetFloat(Enable, value);
        }
    }
}
