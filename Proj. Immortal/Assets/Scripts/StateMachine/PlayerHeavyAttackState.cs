using UnityEngine;

public class PlayerHeavyAttackState : PlayerBaseState
{
    private float _chargeTimer;
    private bool _isCharging;
    private bool _isAnimationFinished;

    public PlayerHeavyAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    { 
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("HeavyAttackState");
        _chargeTimer = 0f;
        _isCharging = true;
        _isAnimationFinished = false;

        Ctx.Animator.SetBool("IsChargingHeavyAttack", _isCharging);

        Ctx.Animator.CrossFadeInFixedTime("HeavyAttack_Charge", 0.1f, 0, 0f);
    }
    public override void UpdateState()
    {
        if (_isCharging)
        {
            _chargeTimer += Time.deltaTime;

            float maxCharge = Ctx.PlayerManager.Combat.CurrentWeaponConfig.MaxChargeTime;

            if (!Ctx.Input.HeavyAttackPressed || _chargeTimer >= maxCharge)
            {
                _chargeTimer = Mathf.Min(_chargeTimer, maxCharge);
                ExecuteHeavySwing();
            }
        }

        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        Ctx.StopMovement();
    }
    public override void ExitState()
    {
        Ctx.PlayerManager.Combat.SetAttackMultipliers(1f, 1f);

        Ctx.Animator.SetBool("IsChargingHeavyAttack", false);
    }
    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {
        if (_isAnimationFinished)
        {
            SwitchState(Factory.Grounded());
        }
    }

    private void ExecuteHeavySwing()
    {
        _isCharging = false;
      
        Ctx.PlayerManager.Stamina.UseStamina(Ctx.Config.heavyAttackStamina);

        float chargePercent = _chargeTimer / Ctx.PlayerManager.Combat.CurrentWeaponConfig.MaxChargeTime;
        float damageMult = Mathf.Lerp(Ctx.PlayerManager.Combat.CurrentWeaponConfig.MinDamageMultiplier, Ctx.PlayerManager.Combat.CurrentWeaponConfig.MaxDamageMultiplier, chargePercent);
        float poiseMult = Mathf.Lerp(1f, Ctx.PlayerManager.Combat.CurrentWeaponConfig.MaxPoiseMultiplier, chargePercent);

        Ctx.PlayerManager.Combat.SetAttackMultipliers(damageMult, poiseMult);
        Ctx.Animator.SetBool("IsChargingHeavyAttack", _isCharging);

        Ctx.Animator.CrossFadeInFixedTime("HeavyAttack_Swing", 0.1f, 0, 0f);
    }

    public void AnimationFinished()
    {
        _isAnimationFinished = true;
    }
}
