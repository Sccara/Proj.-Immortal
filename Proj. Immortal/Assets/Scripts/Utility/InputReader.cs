using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Action OnInteractPressed;
    public Action OnJumpPressed;
    public Action OnCycleQuickItemPressed;
    public Action OnEscapePressed;
    public Action OnInventoryPressed;
    public Action OnDashPressed;

    public Vector2 MoveInput { get; private set; }
    public bool IsMovementPressed => MoveInput.sqrMagnitude > 0.01f;
    public bool LightAttackPressed { get; private set; }
    public bool HeavyAttackPressed { get; private set; }
    public bool IsSprinting { get; private set; }

    private InputSystem_Actions _input;

    private InputAction _moveAction;
    private InputAction _dashAction;
    private InputAction _interactAction;
    private InputAction _cycleQuickItemAction;
    private InputAction _escapeButtonAction;
    private InputAction _inventoryAction;
    private InputAction _lightAttackAction;
    private InputAction _heavyAttackAction;

    private void Awake()
    {
        _input = new InputSystem_Actions();

        _input.Player.HeavyAttack.started += ctx => HeavyAttackPressed = true;
        _input.Player.HeavyAttack.canceled += ctx => HeavyAttackPressed = false;

        _input.Player.Sprint.performed += ctx => IsSprinting = true;
        _input.Player.Sprint.canceled += ctx => IsSprinting = false;

        _input.Player.Interact.performed += ctx => OnInteractPressed?.Invoke();
        _input.Player.CycleQuickItem.performed += ctx => OnCycleQuickItemPressed?.Invoke();
        _input.Player.Escape.performed += ctx => OnEscapePressed?.Invoke();
        _input.Player.Inventory.performed += ctx => OnInventoryPressed?.Invoke();
        _input.Player.Dash.performed += ctx => OnDashPressed?.Invoke();
        _input.Player.Attack.performed += ctx => LightAttackPressed = true;
        _input.Player.Jump.performed += ctx => OnJumpPressed?.Invoke();
    }

    private void Update()
    {
        MoveInput = _input.Player.Move.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _input?.Enable();
    }

    private void OnDisable()
    {
        _input?.Disable();
    }

    public void UseAttackInput() => LightAttackPressed = false;
}
