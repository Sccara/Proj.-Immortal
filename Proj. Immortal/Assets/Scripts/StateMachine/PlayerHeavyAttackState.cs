using UnityEngine;

public class PlayerHeavyAttackState : PlayerBaseState
{
    private float _chargeTimer;
    private bool _hasAttacked;

    public PlayerHeavyAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    { 
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("HeavyAttackState");
        _chargeTimer = 0f;
        _hasAttacked = false;
    }
    public override void UpdateState()
    {
        if (Ctx.Input.HeavyAttackPressed && !_hasAttacked)
        {
            _chargeTimer = Mathf.Min(_chargeTimer + Time.deltaTime, Ctx.Stats.MaxChargeTime);
        }
        else if (!Ctx.Input.HeavyAttackPressed && !_hasAttacked)
        {
            ExecuteHeavyAttack();
        }

        if (_hasAttacked)
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
        if (_hasAttacked)
            SwitchState(Factory.Grounded());
    }

    private void ExecuteHeavyAttack()
    {
        _hasAttacked = true;
        float chargePercent = _chargeTimer / Ctx.Stats.MaxChargeTime;

        float damageMult = Mathf.Lerp(Ctx.Stats.MinDamageMultiplier, Ctx.Stats.MaxDamageMultiplier, chargePercent);
        float poiseMult = Mathf.Lerp(1f, Ctx.Stats.MaxPoiseMultiplier, chargePercent);

        Ctx.PlayerManager.Combat.PerformAttackk(
            damageMult, poiseMult, Ctx.Stats.HeavyAttackStamina,
            Ctx.Stats.AttackStepForce * 1.5f, Ctx.Stats.AttackRange, Ctx.Stats.KnockbackStrength * 1.2f
        );
    }
}
