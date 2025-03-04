using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    // ���� ������ �������� �� �������������� ������ ��� ��������
    [SerializeField] private InputManager _inputManagerPrefab;
    [SerializeField] private Camera _camera;
    [SerializeField] private MapScroll _mapScroll;

    private void Awake()
    {
        InputManager inputManager = Instantiate(_inputManagerPrefab);
        inputManager.Initialize(_camera);

        DragAndDrop[] draggableObjects = FindObjectsOfType<DragAndDrop>();

        if (_mapScroll != null) _mapScroll.Initialize(inputManager, _camera);

        foreach (var obj in draggableObjects)
        {
            obj.Initialize(inputManager, _camera);
        }
    }
}