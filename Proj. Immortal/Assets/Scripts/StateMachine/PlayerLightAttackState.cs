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
        Debug.Log("Light attack state");

        // 1. Запускаем анимацию (через триггер или имя)
        Ctx.Animator.Play("LightAttack"); 

        // 2. Нам всё еще нужен таймер, чтобы знать, когда выйти из стейта
        _attackTimer = 0.8f; // Длина всей анимации

        //_attackTimer = Ctx.Stats.LightAttackCooldown;

        //_startRotation = Ctx.PlayerManager.Combat.WeaponDetector.transform.localRotation;
        //_targetRotation = _startRotation * Quaternion.Euler(0, 90, 0);

        //Ctx.PlayerManager.Combat
        //    .ActivateWeapon(1f, 1f, Ctx.Stats.AttackStamina, Ctx.Stats.AttackStepForce);

        ////Ctx.PlayerManager.Combat.PerformAttackk(1f, 1f, Ctx.Stats.AttackStamina,
        //    //Ctx.Stats.AttackStepForce, Ctx.Stats.AttackRange, Ctx.Stats.KnockbackStrength);
    }
    public override void UpdateState()
    {
        //_attackTimer -= Time.deltaTime;
        //float t = 1f - (_attackTimer / 0.3f); // прогресс от 0 до 1
        //Ctx.PlayerManager.Combat.WeaponDetector.transform.localRotation = Quaternion.Slerp(_startRotation, _targetRotation, t);

        //if (_attackTimer <= 0)
        //{
        //    Ctx.PlayerManager.Combat.DeactivateWeapon();

        //    Ctx.PlayerManager.Combat.WeaponDetector.transform.localRotation = _startRotation;
        //}

        //CheckSwitchStates();

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
