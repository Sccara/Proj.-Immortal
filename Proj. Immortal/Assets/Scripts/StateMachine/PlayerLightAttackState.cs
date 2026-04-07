using UnityEngine;

public class PlayerLightAttackState : PlayerBaseState
{
    private float _attackTimer;

    private Quaternion _startRotation;
    private Quaternion _targetRotation;

    public PlayerLightAttackState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    { 
        IsRootState = true;
    }

    public override void EnterState()
    {
        Ctx.Animator.Play("LightAttack");
        Ctx.PlayerManager.Stamina.UseStamina(Ctx.Stats.AttackStamina);
        _attackTimer = 0.8f; // Длина всей анимации
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
