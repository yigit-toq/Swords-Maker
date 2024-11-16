using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    public float speed;
    public float boundary;
    public float smoothTime;

    private Vector2 touchDelta;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    private InputAction moveAction;

    private void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        moveAction.Enable();
    }

    private void Update()
    {
        touchDelta = moveAction.ReadValue<Vector2>();

        float movement = touchDelta.x * speed * Time.deltaTime;
        targetPosition = transform.position + new Vector3(movement, 0f, 0f);

        targetPosition.x = Mathf.Clamp(targetPosition.x, -boundary, boundary);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
