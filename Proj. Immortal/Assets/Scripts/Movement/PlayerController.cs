using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    [Header("Input")]
    public InputActionAsset InputActions;

    private Transform _camera;
    private InputAction _moveAction;
    private Rigidbody _rb;
    private Vector2 _moveInput;


    private void OnEnable()
    {
        InputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        InputActions.FindActionMap("Player").Disable();
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main.transform;
        _moveAction = InputSystem.actions.FindAction("Move");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = GetMoveDirection();

        if (direction.sqrMagnitude > 0.01f)
        {
            ApplyMovement(direction);
            ApplyRotate(direction);
        }
        else
        {
            _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
        }
    }

    public Vector3 GetMoveDirection()
    {
        Vector3 forward = _camera.forward;
        Vector3 right = _camera.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * _moveInput.y + right * _moveInput.x;

        return moveDirection;
    }

    private void ApplyMovement(Vector3 direction)
    {
        Vector3 velocity = direction * moveSpeed;
        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);
    }

    private void ApplyRotate(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, rotateSpeed);
    }
}
