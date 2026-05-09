using System.Collections;
using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    private float _rollTimer;
    private Vector3 _rollDirection;
    private int _originalLayer;

    public PlayerRollState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) 
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        _rollTimer = 0f;
        Ctx.IsRolling = true;

        Ctx.PlayerManager.Stamina.UseStamina(Ctx.Config.rollStamina);
        Ctx.ImpulseSource.GenerateImpulse();    

        Ctx.PlayerManager.Health.IsInvulnerable = true;

        _originalLayer = Ctx.gameObject.layer;
        Ctx.gameObject.layer = LayerMask.NameToLayer("PlayerRolling");

        _rollDirection = Ctx.GetMoveDirection();

        if (_rollDirection == Vector3.zero)
        {
            _rollDirection = Ctx.transform.forward;
        }

        Ctx.Animator.CrossFadeInFixedTime("Roll", 0.1f, 0, 0f);
    }
    public override void UpdateState()
    {
        _rollTimer += Time.deltaTime;

        if (_rollDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_rollDirection);
            Ctx.transform.rotation = Quaternion.Slerp(Ctx.transform.rotation, targetRotation, 15 * Time.deltaTime); // Refactor 15
        }

        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        float normalizedTime = _rollTimer / Ctx.Config.rollDuration;

        float curveMultiplier = Ctx.Config.rollSpeedCurve.Evaluate(normalizedTime);
        float currentSpeed = curveMultiplier * Ctx.Config.rollBaseSpeed;

        Vector3 newVelocity = _rollDirection * currentSpeed;
        newVelocity.y = Ctx.Rb.linearVelocity.y;

        Ctx.Rb.linearVelocity = newVelocity;
    }
    public override void ExitState()
    {
        Ctx.PlayerManager.Health.IsInvulnerable = false;
        Ctx.IsRolling = false;

        Ctx.gameObject.layer = _originalLayer;
    }
    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {
        if (_rollTimer >= Ctx.Config.rollDuration) 
        {
            SwitchState(Factory.Grounded());
        }
    }
}
