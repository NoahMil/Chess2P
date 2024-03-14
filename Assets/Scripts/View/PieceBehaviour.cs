using System;
using UnityEngine;

namespace View
{
    public class PieceBehaviour: MonoBehaviour
    {
        private MeshRenderer _mesh;

        private static readonly int FresnelColor = Shader.PropertyToID("_fresnelColor");
        private static readonly int EnableFresnel = Shader.PropertyToID("_enableFresnel");
        private static readonly int FresnelAltColor = Shader.PropertyToID("_fresnelAltColor");

        private static Color _initialColor;

        private void Awake()
        {
            _mesh = GetComponent<MeshRenderer>();
            _initialColor = _mesh.material.GetColor(FresnelColor);
        }

        public void Highlight(bool enable)
        {
            int value = Convert.ToInt32(enable);
            _mesh.material.SetColor(FresnelColor, _initialColor);
            _mesh.material.SetFloat(EnableFresnel, value);
        }

        public void HighlightError(bool enable)
        {
            int value = Convert.ToInt32(enable);

            Color altColor = _mesh.material.GetColor(FresnelAltColor);
            
            _mesh.material.SetColor(FresnelColor, altColor);
            _mesh.material.SetFloat(EnableFresnel, value);
        }
    }
}
