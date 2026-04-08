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
        //if (Ctx.Input.DashPressed && !Ctx.RequireNewDashPress && Ctx.DashCooldownTimer <= 0)
        //{
        //    SwitchState(Factory.Dash());
        //}

        if (Ctx.Input.LightAttackPressed)
        {
            Ctx.Input.UseAttackInput();
            SwitchState(Factory.LightAttack());
        }
        else if (Ctx.Input.HeavyAttackPressed)
        {
            SwitchState(Factory.HeavyAttack());
        }
    }

    public override bool HandleInput(InputCommand command)
    {
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
                SwitchState(Factory.Jump());
                break;
                //case InputCommand.LightAttack:
                //    SwitchState(Factory.LightAttack());
                //    return true;
                //case InputCommand.HeavyAttack:
                //    SwitchState(Factory.HeavyAttack());
                //    return true;
        }

        // Если дошли сюда, значит этот стейт не знает, что делать с командой.
        // Передаем её дальше в SubState (например, в Idle или Walk)
        return base.HandleInput(command);
    }
}
