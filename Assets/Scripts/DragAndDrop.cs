using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    //���� ������ �������� �� ����������� ������������ �������
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private float _rayHeight;
    [SerializeField] private float _moveSpeed;

    private Vector3 _offset;
    private Vector3 _lastHitPoint;
    private Camera _camera;
    private InputManager _inputManager;
    private bool _isMoving = false;

    public void Initialize(InputManager inputManager, Camera camera)
    {
        _inputManager = inputManager;
        _camera = camera;
        _lastHitPoint = transform.position;
    }

    // ����� �� ��������� ���������� �� inputManager, ����� ����� ��������� ����� �� �� ������ ��������
    private void Update()
    {
        if (_inputManager == null || !_inputManager.IsDraggingObject) return;

        Vector3 mousePosition = _inputManager.GetMouseWorldPosition();
        transform.position = new Vector3(mousePosition.x + _offset.x, mousePosition.y + _offset.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        ItemMove();
    }

    //����� ��� �������� ���� ���� ������� ���� �� inputManager
    private void OnMouseDown()
    {
        Vector3 mousePosition = _inputManager.GetMouseWorldPosition();
        if (_inputManager != null && _inputManager.CheckDragStart(mousePosition))
        {
            _offset = transform.position - mousePosition;
            _isMoving = false;
        }
    }
    //����� ��� ���������� �������, �� �������� ����� TrySetLastHitPoint(); ������� � ���� �������, ���������� ������ � ����� ��� ��������� ��� ��� ������� ������ �� ����� collisionLayer 
    private void OnMouseUp()
    {
        if (_inputManager == null || !_inputManager.IsDraggingObject) return;
        TrySetLastHitPoint();
        _isMoving = true;
        _inputManager.EndDrag();
    }

    private void ItemMove()
    {
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, _lastHitPoint, _moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _lastHitPoint) < 0.01f)
            {
                _isMoving = false;
                transform.position = _lastHitPoint;
            }
        }
    }

    private void TrySetLastHitPoint()
    {
        Vector3 rayStartPosition = transform.position - Vector3.up * _rayHeight;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, Vector2.down, 0.01f, _collisionLayer);
        if (hit.collider != null)
        {
            _lastHitPoint = hit.point;
        }
    }
    private void OnDrawGizmos()
    {
        Vector3 rayStartPosition = transform.position - Vector3.up * _rayHeight;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(rayStartPosition, rayStartPosition + Vector3.down * 0.01f);
    }
}