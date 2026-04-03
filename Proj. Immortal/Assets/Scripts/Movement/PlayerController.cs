using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashDuration;
    [SerializeField] private string enemyLayerName;

    [Header("Input")]
    public InputActionAsset InputActions;

    private CinemachineImpulseSource _impulseSource;
    private TrailRenderer trail;
    private Transform _camera;
    private InputAction _moveAction;
    private InputAction _dashAction;
    private Rigidbody _rb;
    private Vector2 _moveInput;

    private bool _isDashing;
    private float _dashCooldownTimer;

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
        trail = GetComponent<TrailRenderer>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _camera = Camera.main.transform;
        _moveAction = InputSystem.actions.FindAction("Move");
        _dashAction = InputSystem.actions.FindAction("Dash");
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        trail.emitting = false;
    }

    private void Update()
    {
        if (_isDashing)
            return;

        _moveInput = _moveAction.ReadValue<Vector2>();

        if (_dashCooldownTimer > 0)
        {
            _dashCooldownTimer -= Time.deltaTime;
        }

        if (_dashAction.WasPressedThisFrame() && _dashCooldownTimer <= 0)
        {
            StartCoroutine(PerformDash());
        }
    }

    private void FixedUpdate()
    {
        if (_isDashing)
            return;

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

    private IEnumerator PerformDash()
    {
        _isDashing = true;
        _dashCooldownTimer = dashCooldown;
        trail.emitting = true;
        _impulseSource.GenerateImpulse();

        int playerLayer = gameObject.layer;
        int enemyLayer = LayerMask.NameToLayer(enemyLayerName);

        Physics.IgnoreLayerCollision(playerLayer, enemyLayer, true);

        Vector3 dashDirection = GetMoveDirection();

        if (dashDirection == Vector3.zero)
            dashDirection = transform.forward;

        _rb.linearVelocity = dashDirection * dashForce;

        //_rb.AddForceAtPosition(GetMoveDirection() * dashForce, transform.position, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        Physics.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        _isDashing = false;
        trail.emitting = false;
    }
}
