using UnityEngine;

public class EnemyStunState : EnemyBaseState
{
    private float _stunTimer;

    public EnemyStunState(EnemyStateMachine currentContext, EnemyStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Enemy stun state");
        _stunTimer = 0f;

        Ctx.Agent.enabled = false;
        Ctx.Rb.isKinematic = false;
        Ctx.Rb.linearVelocity = Vector3.zero;

        Ctx.Animator.CrossFadeInFixedTime("Stun", 0.1f, 0, 0f);
    }


    public override void UpdateState()
    {
        _stunTimer += Time.deltaTime;

        if (_stunTimer >= Ctx.Config.StunDuration)
        {
            SwitchState(Factory.Chase());
        }
    }

    public override void CheckSwitchStates()
    {

    }

    public override void ExitState()
    {
        Ctx.Poise.ResetPoise();

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
