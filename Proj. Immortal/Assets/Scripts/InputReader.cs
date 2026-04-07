using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    [Header("Input")]
    public InputActionAsset InputActions;

    private InputAction _moveAction;
    private InputAction _dashAction;
    private InputAction _interactAction;
    private InputAction _cycleQuickItemAction;
    private InputAction _escapeButtonAction;
    private InputAction _inventoryAction;
    private InputAction _lightAttackAction;
    private InputAction _heavyAttackAction;

    public Vector2 MoveInput { get; private set; }
    public bool DashPressed { get; private set; }
    public bool InteractPressed { get; private set; }
    public bool CycleQuickItemAction { get; set; }
    public bool EscapeButtonAction { get; set; }
    public bool InventoryAction { get; set; }
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
        _interactAction = actions.FindAction("Interact");
        _lightAttackAction = actions.FindAction("Attack");
        _heavyAttackAction = actions.FindAction("HeavyAttack");
        _cycleQuickItemAction = actions.FindAction("CycleQuickItem");
        _escapeButtonAction = actions.FindAction("Escape");
        _inventoryAction = actions.FindAction("Inventory");

        _interactAction.started += ctx => InteractPressed = true;
        _interactAction.canceled += ctx => InteractPressed = false;

        _escapeButtonAction.started += ctx => EscapeButtonAction = true;
        _escapeButtonAction.canceled += ctx => EscapeButtonAction = false;

        _inventoryAction.started += ctx => InventoryAction = true;
        _inventoryAction.canceled += ctx => InventoryAction = false;

        _cycleQuickItemAction.started += ctx => CycleQuickItemAction = true;
        _cycleQuickItemAction.canceled += ctx => CycleQuickItemAction = false;

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
