using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Debug.Log("GroundedState");
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
        if (!Ctx.Input.IsMovementPressed)
        {
            SetSubState(Factory.Idle());
        }
        else
        {
            SetSubState(Factory.Walk());
        }
    }
    public override void CheckSwitchStates()
    {

    }

    public override bool HandleInput(InputCommand command)
    {
        Debug.Log($"Command: {command}");

        switch (command)
        {
            case InputCommand.Dash:
                if (Ctx.DashCooldownTimer <= 0)
                {
                    SwitchState(Factory.Dash());
                    return true; 
                }
                break;
            case InputCommand.Jump:
                if (Ctx.PlayerManager.Stamina.HasEnoughStamina())
                {
                    Ctx.IsSprintJump = Ctx.Input.IsSprinting && !Ctx.IsSprintBroken;
                    SwitchState(Factory.Jump());
                    return true;
                }
                break;
            case InputCommand.LightAttack:
                SwitchState(Factory.LightAttack());
                return true;
            case InputCommand.HeavyAttack:
                SwitchState(Factory.HeavyAttack());
                return true;
        }

        return base.HandleInput(command);
    }
}
