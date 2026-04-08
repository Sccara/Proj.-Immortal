using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Run state");
        Ctx.Stats.MoveSpeed = Ctx.Stats.SprintMoveSpeed;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
        Ctx.PlayerManager.Stamina.UseStamina(Ctx.Stats.SprintStamina * Time.deltaTime);
    }
    public override void FixedUpdateState()
    {
        Vector3 direction = Ctx.GetMoveDirection();
        Ctx.ApplyMovement(direction, Ctx.Stats.MoveSpeed);
        Ctx.ApplyRotate(direction);
    }
    public override void ExitState()
    {
        Ctx.Stats.MoveSpeed = Ctx.Stats.WalkMoveSpeed;
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
        else if ((Ctx.Input.IsMovementPressed && !Ctx.Input.IsSprinting) || !Ctx.PlayerManager.Stamina.HasEnoughStamina())
        {
            SwitchState(Factory.Walk());
        } 
    }
}
