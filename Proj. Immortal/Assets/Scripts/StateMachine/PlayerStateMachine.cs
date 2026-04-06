using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    private PlayerStats _stats;
    private InputReader _input;
    private PlayerManager _playerManager;

    public PlayerBaseState CurrentState { get => _currentState; set { _currentState = value; } }
    public float DashCooldownTimer { get => _dashCooldownTimer; set { _dashCooldownTimer = value; } }
    public TrailRenderer Trail => trail;
    public CinemachineImpulseSource ImpulseSource => _impulseSource;
    public PlayerManager PlayerManager => _playerManager;
    public PlayerStats Stats => _stats;
    public InputReader Input => _input;
    public int PlayerLayer => gameObject.layer;
    public string EnemyLayerName => enemyLayerName;
    public Rigidbody Rb => _rb;
    public bool IsDashing { get => _isDashing; set { _isDashing = value; } }
    public bool RequireNewDashPress { get => _requireNewDashPress; set { _requireNewDashPress = value; } }


    [Header("Settings")]
    [SerializeField] private string enemyLayerName;
    [SerializeField] private bool _requireNewDashPress;

    [Header("Input")]
    public InputActionAsset InputActions;

    private CinemachineImpulseSource _impulseSource;
    private TrailRenderer trail;
    private Transform _camera;
    private Rigidbody _rb;

    private bool _isDashing;
    private float _dashCooldownTimer;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
        _rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        _playerManager = GetComponent<PlayerManager>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _camera = Camera.main.transform;

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState(); 
    }

    private void Start()
    {
        _stats = PlayerStats.Instance;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (_dashCooldownTimer > 0)
        {
            _dashCooldownTimer -= Time.deltaTime;
        }

        _currentState.UpdateStates();
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdateStates();

        //if (_isDashing)
        //    return;

        //Vector3 direction = GetMoveDirection();

        //if (direction.sqrMagnitude > 0.01f)
        //{
        //    ApplyMovement(direction);
        //    ApplyRotate(direction);
        //}
        //else
        //{
        //    _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
        //}
    }

    public Vector3 GetMoveDirection()
    {
        Vector3 forward = _camera.forward;
        Vector3 right = _camera.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * Input.MoveInput.y + right * Input.MoveInput.x;

        return moveDirection;
    }

    public void ApplyMovement(Vector3 direction, float speed)
    {
        Vector3 velocity = direction * speed;
        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);
    }

    public void StopMovement()
    {
        _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
    }

    public void ApplyRotate(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, _stats.RotateSpeed);
    }
}
