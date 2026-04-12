using UnityEngine;

public class PlayerUseItemState : PlayerBaseState
{
    private bool _isAnimationFinished;

    public PlayerUseItemState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Use Item State");
        _isAnimationFinished = false;
        Ctx.Stats.MoveSpeed = Ctx.Stats.UseItemSpeed;

        ItemSO activeItem = Ctx.PlayerManager.QuickItems.GetCurrentItem();

        Ctx.Animator.Play(activeItem.AnimationTriggerName);
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
        Ctx.Stats.MoveSpeed = Ctx.Stats.WalkMoveSpeed;
    }
    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {
        if (_isAnimationFinished)
            SwitchState(Factory.Grounded());
    }

    public void AnimationFinished() => _isAnimationFinished = true;
}
