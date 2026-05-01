using UnityEngine;

public class PlayerCastState : PlayerBaseState
{
    private SpellInstance _currentSpell;
    private bool _hasCastFired;

    public PlayerCastState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
: base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Cast state");
        Ctx.StopMovement();
        _hasCastFired = false;
        _currentSpell = Ctx.PlayerManager.SpellMemory.CurrentSpell;

        float manaCost = _currentSpell.SpellData.ManaCost;

        if (Ctx.PlayerManager.Attributes.ManaResource.Current < manaCost)
        {
            Ctx.Animator.Play("Cast_Fail");
            return;
        }

        string animTrigger = _currentSpell.SpellData.AnimationTriggerName;
        Ctx.Animator.Play(animTrigger);
        _hasCastFired = true;
    }
    public override void UpdateState()
    {
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
        if (_hasCastFired)
        {
            SwitchState(Factory.Grounded());
        }
    }

    public void AnimationFinished()
    {
        _hasCastFired = true;
    }
}
