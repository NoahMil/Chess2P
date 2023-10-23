using System;
using UnityEngine;

public class PieceBehaviour: MonoBehaviour
{
    private MeshRenderer _mesh;
    private static readonly int EnableFresnel = Shader.PropertyToID("_enableFresnel");

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        
    }

    public void Highlight(bool enable)
    {
        int value = Convert.ToInt32(enable);
        _mesh.material.SetFloat(EnableFresnel, value);
    }
}
