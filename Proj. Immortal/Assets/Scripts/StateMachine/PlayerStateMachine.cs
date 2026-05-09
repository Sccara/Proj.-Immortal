using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerBaseState _currentState;
    private PlayerStateFactory _states;
    private InputReader _input;
    private PlayerManager _playerManager;
    [SerializeField] private Animator _animator;
    private TargetLockSystem _targetLock;

    // Вынести в отдельный скрипт
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private PlayerConfigSO config;

    [SerializeField] private TextMeshProUGUI stateText;


    public PlayerBaseState CurrentState { get => _currentState; set { _currentState = value; } }
    public PlayerStateFactory States { get => _states; }   
    public float DashCooldownTimer { get => _dashCooldownTimer; set { _dashCooldownTimer = value; } }
    public TrailRenderer Trail => trail;
    public PlayerConfigSO Config => config;
    public CinemachineImpulseSource ImpulseSource => _impulseSource;
    public PlayerManager PlayerManager => _playerManager;
    public InputReader Input => _input;
    public Animator Animator => _animator;
    public int PlayerLayer => gameObject.layer;
    public string EnemyLayerName => enemyLayerName;
    public Rigidbody Rb => _rb;
    public bool IsSprintBroken { get; set; }
    public bool IsSprintJump { get; set; }
    public bool IsRolling { get => _isRolling; set { _isRolling = value; } }
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

    private bool _isRolling;
    private float _dashCooldownTimer;

    private void Awake()
    {
        _input = GetComponent<InputReader>();
        _rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
        _targetLock = GetComponent<TargetLockSystem>();
        _playerManager = GetComponent<PlayerManager>();
        _impulseSource = GetComponent<CinemachineImpulseSource>();
        _camera = Camera.main.transform;

        _states = new PlayerStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Input.OnRollPressed += () => CurrentState?.HandleInput(InputCommand.Roll);
        Input.OnJumpPressed += () => CurrentState?.HandleInput(InputCommand.Jump);
        Input.OnLightAttackPressed += () => CurrentState?.HandleInput(InputCommand.LightAttack);
        Input.OnHeavyAttackPressed += () => CurrentState?.HandleInput(InputCommand.HeavyAttack);
        Input.OnQuickItemUsePressed += () => CurrentState?.HandleInput(InputCommand.UseItem);
    }

    private void Update()
    {
        if (_dashCooldownTimer > 0)
        {
            _dashCooldownTimer -= Time.deltaTime;
        }

        _currentState.UpdateStates();

        stateText.text = $"Current State: {_currentState}";
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdateStates();
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

        float currentSpeedRatio = velocity.magnitude / PlayerManager.Attributes.MoveSpeedStat.BaseValue;
        Animator.SetFloat("Speed", currentSpeedRatio);
        _rb.linearVelocity = new Vector3(velocity.x, _rb.linearVelocity.y, velocity.z);
    }

    public void StopMovement()
    {
        _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);

        float currentSpeedRatio = Vector3.zero.magnitude / PlayerManager.Attributes.MoveSpeedStat.BaseValue;
        Animator.SetFloat("Speed", currentSpeedRatio);
    }

    public void ApplyRotate(Vector3 direction)
    {
        if (_targetLock.IsLockedOn)
        {
            Vector3 directionToTarget = _targetLock.CurrentTarget.position - transform.position;
            directionToTarget.y = 0;

            if (directionToTarget != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Config.rotateSpeed * Time.fixedDeltaTime);
            }
        }
        else if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, Config.rotateSpeed);
        }
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheckTransform.position, groundCheckRadius, groundMask);
    }
}

public enum InputCommand
{
    Roll,
    LightAttack,
    HeavyAttack,
    Interact,
    Heal,
    Jump,
    Sprint,
    UseItem
}