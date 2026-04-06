using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [Header("Input")]
    public InputActionAsset InputActions;

    private InputAction _moveAction;
    private InputAction _dashAction;
    private InputAction _lightAttackAction;
    private InputAction _heavyAttackAction;

    public Vector2 MoveInput { get; private set; }
    public bool DashPressed { get; private set; }
    public bool IsMovementPressed => MoveInput.sqrMagnitude > 0.01f;
    public bool LightAttackPressed { get; private set; }
    public bool HeavyAttackPressed { get; private set; }


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
        var actions = InputSystem.actions;

        _moveAction = actions.FindAction("Move");
        _dashAction = actions.FindAction("Dash");
        _lightAttackAction = actions.FindAction("Attack");
        _heavyAttackAction = actions.FindAction("HeavyAttack");

        _dashAction.started += ctx => DashPressed = true;
        _dashAction.canceled += ctx => DashPressed = false;

        _lightAttackAction.performed += ctx => LightAttackPressed = true;

        _heavyAttackAction.started += ctx => HeavyAttackPressed = true;
        _heavyAttackAction.canceled += ctx => HeavyAttackPressed = false;
    }

    private void Update()
    {
        MoveInput = _moveAction.ReadValue<Vector2>();
    }

    public void UseAttackInput() => LightAttackPressed = false;
}
