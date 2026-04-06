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

        if(Ctx.Input.DashPressed && Ctx.DashCooldownTimer <= 0)
    {
            SwitchState(Factory.Dash());
        }
        else if (Ctx.Input.LightAttackPressed)
        {
            Ctx.Input.UseAttackInput();
            SwitchState(Factory.LightAttack());
        }
        else if (Ctx.Input.HeavyAttackPressed)
        {
            SwitchState(Factory.HeavyAttack());
        }
    }
}
