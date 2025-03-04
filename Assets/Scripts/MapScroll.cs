using UnityEngine;

public class MapScroll : MonoBehaviour
{
    //Этот скрипт отвечает за скролинг карты
    [SerializeField] private float _scrollSpeed = 5f;
    [SerializeField] private float _minX = -10f;
    [SerializeField] private float _maxX = 10f;
    private Vector3 _lastMousePosition;
    private Camera _camera;
    private InputManager _inputManager;

    public void Initialize(InputManager inputManager, Camera camera)
    {
        _inputManager = inputManager;
        _camera = camera;
    }

    private void Update()
    {
        if (_inputManager == null || _inputManager.IsDraggingObject) return;

        if (Input.GetMouseButtonDown(0))
        {
            _lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - _lastMousePosition;
            MoveCamera(delta);
            _lastMousePosition = Input.mousePosition;
        }
    }

    private void MoveCamera(Vector3 delta)
    {
        Vector3 newPosition = _camera.transform.position + new Vector3(-delta.x, 0, 0) * _scrollSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, _minX, _maxX);
        _camera.transform.position = newPosition;
    }
}
