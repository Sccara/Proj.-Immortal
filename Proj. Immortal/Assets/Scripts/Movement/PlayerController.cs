using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputActionAsset InputActions;

    private InputAction _moveAction;
    private InputAction _lookAction;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;

    private Rigidbody _rb;
    private Vector2 _moveInput;
    private Vector2 _lookInput;
    private Vector3 _moveDirection;
    private Vector3 _targetVelocity;

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
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
    }

    private void Update()
    {
        _moveInput = _moveAction.ReadValue<Vector2>();
        _lookInput = _lookAction.ReadValue<Vector2>();

        _targetVelocity = new Vector3(_moveInput.x, 0, _moveInput.y).normalized;
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyRotate();
    }

    private void ApplyMovement()
    {
        Vector3 currentVelocity = _rb.linearVelocity;
        Vector3 moveVelocity = _targetVelocity * moveSpeed;

        _rb.linearVelocity = new Vector3(moveVelocity.x, currentVelocity.y, moveVelocity.z);
    }

    private void ApplyRotate()
    {
        if (_targetVelocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_targetVelocity);
            _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
        }
    }
}
