using UnityEngine;

public class PlayerLightAttackState : PlayerBaseState
{
    private bool _isAnimationFinished;

    public PlayerLightAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    { 
        IsRootState = true;
    }

    public override void EnterState()
    {
        _isAnimationFinished = false;
        Ctx.PlayerManager.Stamina.UseStamina(Ctx.Config.attackStamina);
        Ctx.PlayerManager.Combat.SetAttackMultipliers(1f, 1f);
        Ctx.Animator.Play("LightAttack");
    }
    public override void UpdateState()
    {
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
        if (_isAnimationFinished)
        {
            SwitchState(Factory.Grounded());
        }
    }

    public void AnimationFinished()
    {
        _isAnimationFinished = true;
    }
}
