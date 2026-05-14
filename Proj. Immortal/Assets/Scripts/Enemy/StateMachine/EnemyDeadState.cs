using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine currentContext, EnemyStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Enemy dead state");
        Ctx.Agent.enabled = false;
        Ctx.Sensor.enabled = false;
        Ctx.GetComponent<Collider>().isTrigger = true;
        Ctx.Animator.CrossFadeInFixedTime("Death", 0.1f, 0, 0f);
    }


    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {

    }

    public override void ExitState()
    {

    }

    public override void FixedUpdateState()
    {

    }

    public override void InitializeSubState()
    {

    }
}
