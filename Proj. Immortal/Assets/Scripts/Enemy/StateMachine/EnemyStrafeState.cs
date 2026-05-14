using UnityEngine;

public class EnemyStrafeState : EnemyBaseState
{
    private float _strafeTimer;
    private int _strafeDirection;

    public EnemyStrafeState(EnemyStateMachine currentContext, EnemyStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Enemy strafe state");
        _strafeTimer = 0f;

        _strafeDirection = Random.Range(0, 2) == 0 ? -1 : 1;

        Ctx.Agent.ResetPath();

        Ctx.Animator.CrossFadeInFixedTime("Strafe", 0.1f, 0, 0f);
    }


    public override void UpdateState()
    {
        _strafeTimer += Time.deltaTime; 

        if (Ctx.Sensor.PlayerTransform != null)
        {
            Vector3 dirToPlayer = Ctx.DirectionToPlayer;
            dirToPlayer.y = 0;

            if (dirToPlayer != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(dirToPlayer);
                Ctx.transform.rotation = Quaternion.Slerp(Ctx.transform.rotation, lookRotation, Ctx.Config.RotateSpeed * Time.deltaTime);
            }

            Vector3 rightDir = Vector3.Cross(dirToPlayer, Vector3.up).normalized;

            Vector3 strafeVector = rightDir * _strafeDirection;

            float currentDistance = Vector3.Distance(Ctx.transform.position, Ctx.Sensor.PlayerTransform.position);

            if (currentDistance > Ctx.Config.AttackRange)
            {
                strafeVector += dirToPlayer;
            }

            Ctx.Agent.velocity = strafeVector.normalized * Ctx.Config.WalkMoveSpeed;
        }

        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (_strafeTimer >= Ctx.Config.StrafeWaitTime)
        {
            SwitchState(Factory.Attack());
            return;
        }

        if (Ctx.Sensor.PlayerTransform != null)
        {
            float currentDistance = Vector3.Distance(Ctx.transform.position, Ctx.Sensor.PlayerTransform.position);

            if (currentDistance > Ctx.Config.AttackRange + 2.5f)
            {
                SwitchState(Factory.Chase());
            }
        }
    }

    public override void ExitState()
    {
        Ctx.Agent.velocity = Vector3.zero;
    }

    public override void FixedUpdateState()
    {

    }

    public override void InitializeSubState()
    {
       
    }
}
