using UnityEngine;

public class PlayerCastState : PlayerBaseState
{
    public PlayerCastState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
: base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Cast state");
        Ctx.StopMovement();

    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {

    }
    public override void ExitState()
    {

    }
    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {

    }
}
