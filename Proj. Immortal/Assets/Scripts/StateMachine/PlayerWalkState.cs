using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Walk state");
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        Vector3 direction = Ctx.GetMoveDirection();
        Ctx.ApplyMovement(direction, Ctx.Stats.MoveSpeed);
        Ctx.ApplyRotate(direction);
    }
    public override void ExitState()
    {
        
    }
    public override void InitializeSubState()
    {
        
    }
    public override void CheckSwitchStates()
    {
        if (!Ctx.Input.IsMovementPressed)
        {
            SwitchState(Factory.Idle());
        }
    }
}
