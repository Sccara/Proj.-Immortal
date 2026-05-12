using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Run state");
        Ctx.PlayerManager.Attributes.MoveSpeedStat.BaseValue = Ctx.Config.sprintMoveSpeed;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        Ctx.PlayerManager.Stamina.UseStamina(Ctx.Config.sprintStamina * Time.deltaTime);
    }
    public override void FixedUpdateState()
    {
        Vector3 direction = Ctx.GetMoveDirection();
        Ctx.ApplyMovement(direction, Ctx.PlayerManager.Attributes.MoveSpeedStat.BaseValue);
        Ctx.ApplyRotate(direction);
    }
    public override void ExitState()
    {
        Ctx.PlayerManager.Attributes.MoveSpeedStat.BaseValue = Ctx.Config.walkMoveSpeed;
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
        else if (Ctx.Input.IsMovementPressed && !Ctx.Input.IsSprinting)
        {
            SwitchState(Factory.Walk());
        }
        else if (!Ctx.PlayerManager.Stamina.HasEnoughStamina())
        {
            Ctx.IsSprintBroken = true;
            SwitchState(Factory.Walk());
        }
    }
}
