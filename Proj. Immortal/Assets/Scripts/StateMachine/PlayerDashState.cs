using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    public PlayerDashState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) 
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Ctx.PlayerManager.Health.IsInvulnerable = true;
        Ctx.StartCoroutine(HandleDash());
    }
    public override void UpdateState()
    {

    }
    public override void FixedUpdateState()
    {

    }
    public override void ExitState()
    {
        Ctx.PlayerManager.Health.IsInvulnerable = false;
    }
    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {

    }

    IEnumerator HandleDash()
    {
        Ctx.IsDashing = true;
        Ctx.DashCooldownTimer = Ctx.Config.dashCooldown;
        Ctx.Trail.emitting = true;
        Ctx.ImpulseSource.GenerateImpulse();
        Ctx.PlayerManager.Stamina.UseStamina(Ctx.Config.dashStamina);

        int playerLayer = Ctx.PlayerLayer;
        int enemyLayer = LayerMask.NameToLayer(Ctx.EnemyLayerName);

        Physics.IgnoreLayerCollision(playerLayer, enemyLayer, true);

        Vector3 dashDirection = Ctx.GetMoveDirection();

        if (dashDirection == Vector3.zero)
            dashDirection = Ctx.transform.forward;

        Ctx.Rb.linearVelocity = dashDirection * Ctx.Config.dashForce;

        //_rb.AddForceAtPosition(GetMoveDirection() * dashForce, transform.position, ForceMode.VelocityChange);

        yield return new WaitForSeconds(Ctx.Config.dashDuration);

        Physics.IgnoreLayerCollision(playerLayer, enemyLayer, false);
        Ctx.Trail.emitting = false;
        Ctx.IsDashing = false;
        SwitchState(Factory.Grounded());
    }
}
