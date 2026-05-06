using UnityEngine;

public class PlayerLightAttackState : PlayerBaseState
{
    private bool _isAnimationFinished;
    private float _safetyTimer;
    private float _maxAttackDuration = 2.0f;

    public PlayerLightAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    { 
        IsRootState = true;
    }

    public override void EnterState()
    {
        _isAnimationFinished = false;
        _safetyTimer = 0f;
        Ctx.StopMovement();
        Ctx.PlayerManager.Stamina.UseStamina(Ctx.Config.attackStamina);
        Ctx.PlayerManager.Combat.SetAttackMultipliers(1f, 1f);
        float weaponAttackSpeed = 2.0f; // Get from weapon stats
        Ctx.Animator.SetFloat("AttackSpeedMultiplier", weaponAttackSpeed);
        Ctx.Animator.CrossFadeInFixedTime("LightAttack_1", 0.1f, 0, 0f);
    }
    public override void UpdateState()
    {
        _safetyTimer += Time.deltaTime;

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

    }
    public override void CheckSwitchStates()
    {
        if (_isAnimationFinished || _safetyTimer > _maxAttackDuration)
        {
            SwitchState(Factory.Grounded());
        }
    }

    public void AnimationFinished()
    {
        Debug.Log("Light attack animation finished");
        _isAnimationFinished = true;
    }
}
