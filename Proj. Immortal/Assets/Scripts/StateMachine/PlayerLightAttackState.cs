using UnityEngine;

public class PlayerLightAttackState : PlayerBaseState
{
    private float _attackTimer;

    public PlayerLightAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    { 
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Light attack state");
        _attackTimer = Ctx.Stats.LightAttackCooldown;

        Ctx.PlayerManager.Combat.PerformAttackk(1f, 1f, Ctx.Stats.AttackStamina,
            Ctx.Stats.AttackStepForce, Ctx.Stats.AttackRange, Ctx.Stats.KnockbackStrength);
    }
    public override void UpdateState()
    {
        _attackTimer -= Time.deltaTime;
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        Ctx.StopMovement();
    }
    public override void ExitState()
    {

    }
    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {
        if (_attackTimer <= 0)
        {
            SwitchState(Factory.Grounded());
        }
    }
}
