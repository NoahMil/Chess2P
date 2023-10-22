using System;
using UnityEngine;

public class PieceBehaviour: MonoBehaviour
{
    [SerializeField] private Side _side;

    private MeshRenderer _mesh;

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
        _mesh.material.SetFloat("_enableFresnel", value);
    }
}
