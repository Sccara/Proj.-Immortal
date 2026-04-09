using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
: base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Fall state");

    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        Ctx.Rb.AddForce(Vector3.down * Ctx.Stats.FallMultiplier, ForceMode.Acceleration);

        HandleAirPhysics();
    }
    public override void ExitState()
    {

    }
    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {
        if (Ctx.IsGrounded())
        {
            SwitchState(Factory.Grounded());
        }
    }

    private void HandleAirPhysics()
    {
        Vector3 direction = Ctx.GetMoveDirection();
        float maxAirSpeed = Ctx.IsSprintJump ? Ctx.Stats.SprintMoveSpeed : Ctx.Stats.WalkMoveSpeed;
        Ctx.Rb.AddForce(direction * maxAirSpeed * 0.5f, ForceMode.Acceleration);

        Vector3 currentVelocity = Ctx.Rb.linearVelocity;
        Vector3 horizontalVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);

        if (horizontalVelocity.magnitude > maxAirSpeed)
        {
            Vector3 clampedVelocity = horizontalVelocity.normalized * maxAirSpeed;
            Ctx.Rb.linearVelocity = new Vector3(clampedVelocity.x, currentVelocity.y, clampedVelocity.z);
        }
    }
}
