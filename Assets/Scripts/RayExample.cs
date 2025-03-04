using System.Collections;
using UnityEngine;

public class RayExample : MonoBehaviour
{
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private float rayHeight = 0.5f;
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 offset;
    private Vector3 lastHitPoint;
    private Coroutine moveCoroutine;

    private void Start()
    {
    }

    private void Update()
    {
        Vector3 rayStartPosition = transform.position - Vector3.up * rayHeight;
        Debug.DrawRay(rayStartPosition, Vector2.down * 0.01f, Color.red);

        if (InputManager.Instance.IsDraggingObject)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }
    }

    private void OnMouseDown()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (InputManager.Instance.CheckDragStart(mousePos))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            offset = transform.position - mousePosition;

            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }
        }
    }

    private void OnMouseUp()
    {
        if (!InputManager.Instance.IsDraggingObject) return;

        Vector3 rayStartPosition = transform.position - Vector3.up * rayHeight;
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, Vector2.down, 0.01f, collisionLayer);

        if (hit.collider != null)
        {
            lastHitPoint = hit.point;
        }

        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(SmoothMoveToPosition(lastHitPoint));
        InputManager.Instance.EndDrag();
    }

    private IEnumerator SmoothMoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
    }
}