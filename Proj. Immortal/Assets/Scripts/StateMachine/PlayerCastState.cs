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
        Debug.Log($"Mana: {Ctx.PlayerManager.Attributes.ManaResource.Current}");
        Debug.Log($"Mana Cost: {manaCost}");
        if (Ctx.PlayerManager.Attributes.ManaResource.Current < manaCost)
        {
            //Ctx.Animator.Play("Cast_Fail");
            Ctx.Animator.CrossFadeInFixedTime("Cast_Fail", 0.1f, 0, 0f);
            return;
        }

        string animTrigger = _currentSpell.SpellData.AnimationTriggerName;
        Debug.Log($"Playing animation {animTrigger}");
        //Ctx.Animator.Play(animTrigger);
        Ctx.Animator.CrossFadeInFixedTime(animTrigger, 0.1f, 0, 0f);
        //_hasCastFired = true;
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
            Debug.Log("Switch state to grounded from cast");
            SwitchState(Factory.Grounded());
        }
    }

    public void AnimationFinished()
    {
        _hasCastFired = true;
    }
}
