using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine currentContext, EnemyStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Ctx.StopMovement();

        Debug.Log("Enemy idle state");
    }


    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.Sensor.IsPlayerInViewSight() || Ctx.Sensor.IsPlayerInAgroRadius())
        {
            Debug.Log("Enemy spotted player!");

            SwitchState(Factory.Chase());
        }
    }

    public override void ExitState()
    {
        Ctx.Agent.isStopped = false;
    }

    public override void FixedUpdateState()
    {
  
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

}
