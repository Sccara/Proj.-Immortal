using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public EnemyPatrolState(EnemyStateMachine currentContext, EnemyStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Enemy patrol state");

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
