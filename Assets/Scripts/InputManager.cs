using UnityEngine;

public class InputManager : MonoBehaviour
{
    //Ётот скрипт отвечает за отслеживание зажатий
    [SerializeField] private LayerMask _draggableLayer;
    [SerializeField] private float _pickRadius = 0.2f;
    
    private Camera _camera;
    public bool IsDraggingObject { get; private set; }

    public void Initialize(Camera camera)
    {
        _camera = camera;
    }

    public bool CheckDragStart(Vector2 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(position, _pickRadius, _draggableLayer);
        IsDraggingObject = hit != null;
        return IsDraggingObject;
    }

    public void EndDrag()
    {
        IsDraggingObject = false;
    }

    public Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }
}