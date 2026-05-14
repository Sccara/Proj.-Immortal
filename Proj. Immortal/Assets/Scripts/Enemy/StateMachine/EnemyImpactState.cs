using System.Threading;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private Vector3 _knockback;
    private float _timer;

    public EnemyImpactState(EnemyStateMachine currentContext, EnemyStateFactory playerStateFactory, Vector3 knockback) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        _knockback = knockback;
    }

    public override void EnterState()
    {
        Debug.Log("Enemy impact state");
        _timer = 0f;

        Ctx.Agent.enabled = false;
        Ctx.Rb.isKinematic = false;
        Ctx.Rb.AddForce(_knockback, ForceMode.Impulse);
        Ctx.Animator.CrossFadeInFixedTime("Impact", 0.1f, 0, 0f);
    }


    public override void UpdateState()
    {
        _timer += Time.deltaTime;

        if (_timer >= Ctx.Config.KnockbackDuration)
        {
            SwitchState(Factory.Chase());
        }
    }

    public override void CheckSwitchStates()
    {

    }

    public override void ExitState()
    {
        Ctx.Rb.isKinematic = true;

        Ctx.Agent.enabled = true;
    }

    public override void FixedUpdateState()
    {

    }

    public override void InitializeSubState()
    {

    }
}
