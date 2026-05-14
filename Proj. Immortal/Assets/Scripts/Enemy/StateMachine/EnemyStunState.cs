using UnityEngine;

public class EnemyStunState : EnemyBaseState
{
    public EnemyStunState(EnemyStateMachine currentContext, EnemyStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Enemy stun state");

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
