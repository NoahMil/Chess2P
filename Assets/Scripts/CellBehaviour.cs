using UnityEngine;
using Managers;

public class CellBehaviour: MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
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
}
