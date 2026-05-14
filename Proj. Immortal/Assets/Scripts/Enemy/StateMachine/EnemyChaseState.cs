using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyStateMachine currentContext, EnemyStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Ctx.Agent.isStopped = false;
        Ctx.Agent.speed = Ctx.Config.ChaseSpeed;

        Ctx.Animator.CrossFadeInFixedTime("Movement", 0.1f, 0, 0f);

        Debug.Log("Enemy chase state");
    }


    public override void UpdateState()
    {
        if (Ctx.Sensor.PlayerTransform != null)
        {
            Ctx.Agent.SetDestination(Ctx.Sensor.PlayerTransform.position);

            float currentSpeedRatio = Ctx.Agent.velocity.magnitude / Ctx.Config.ChaseSpeed;
            Ctx.Animator.SetFloat("Speed", currentSpeedRatio);
        }

        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.Sensor.IsLostPlayerSight())
        {
            Debug.Log("Target Lost");
            SwitchState(Factory.Idle());
            return;
        }

        if (Ctx.Sensor.PlayerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(Ctx.transform.position, Ctx.Sensor.PlayerTransform.position);

            if (distanceToPlayer <= Ctx.Config.AttackRange)
            {
                Debug.Log("Player close enough! Combat!");
                
                SwitchState(Factory.Attack());
            }
        }
    }

    public override void ExitState()
    {
        Ctx.Agent.speed = Ctx.Config.WalkMoveSpeed;
    }

    public override void FixedUpdateState()
    {

    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

}
