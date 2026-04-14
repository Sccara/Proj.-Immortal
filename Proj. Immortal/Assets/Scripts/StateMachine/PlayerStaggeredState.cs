using NUnit.Framework.Internal.Execution;
using UnityEngine;

public class PlayerStaggeredState : PlayerBaseState
{
    private float _staggerTimer;

    public PlayerStaggeredState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    { 
        IsRootState = true;
    }

    public override void EnterState()
    {
        Ctx.PlayerManager.Health.CanBeStaggered = false;

        _staggerTimer = Ctx.Config.staggerTime;

        Ctx.StopMovement();

        Ctx.PlayerManager.GetComponent<MeshRenderer>().material.color = Color.red;
    }
    public override void UpdateState()
    {
        _staggerTimer -= Time.deltaTime;
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        Ctx.StopMovement();
    }
    public override void ExitState()
    {
        Ctx.PlayerManager.Health.CanBeStaggered = true;
        Ctx.PlayerManager.Health.TriggerInvulnerability(0.3f);
        Ctx.PlayerManager.Health.ResetPoise();
        Ctx.PlayerManager.GetComponent<MeshRenderer>().material.color = Color.gray;
    }
    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {
        if (_staggerTimer <= 0)
        {
            SwitchState(Factory.Grounded());
        }
    }
}
