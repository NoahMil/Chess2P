using System;
using UnityEngine;

namespace MonoBehaviours
{
    public class PieceBehaviour: MonoBehaviour
    {
        private MeshRenderer _mesh;

        private static readonly int FresnelColor = Shader.PropertyToID("_fresnelColor");
        private static readonly int EnableFresnel = Shader.PropertyToID("_enableFresnel");

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

        public void HighlightError(bool enable, Color color = default)
        {
            int value = Convert.ToInt32(enable);

            if (color == default)
                color = Color.red;

            _mesh.material.SetColor(FresnelColor, enable ? color : _initialColor);
            _mesh.material.SetFloat(EnableFresnel, value);
        }
    }
}
