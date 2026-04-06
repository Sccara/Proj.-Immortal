using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Idle state");
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        Ctx.StopMovement();
    }
    public override void ExitState()
    {
        
    }
    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {
        if (Ctx.Input.IsMovementPressed)
        {
            SwitchState(Factory.Walk());
        }
    }
}
