using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float _attackTimer;
    private bool _isAnimationFinished;

    public EnemyAttackState(EnemyStateMachine currentContext, EnemyStateFactory playerStateFactory) : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Enemy attack state");

        Ctx.Agent.isStopped = true;
        Ctx.Agent.velocity = Vector3.zero;

        _attackTimer = 0;
        _isAnimationFinished = false;

        Ctx.Animator.CrossFadeInFixedTime("Attack", 0.1f, 0, 0f);
    }


    public override void UpdateState()
    {
        _attackTimer += Time.deltaTime;

        if (Ctx.Sensor.PlayerTransform != null && !_isAnimationFinished)
        {
            Vector3 direction = Ctx.DirectionToPlayer;
            direction.y = 0;
            Debug.Log($"direction: {direction}");

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Ctx.transform.rotation = Quaternion.Slerp(Ctx.transform.rotation, lookRotation, Ctx.Config.RotateSpeed * Time.deltaTime);
            }
        }

        // Add Animation
        if (_attackTimer >= 1.5f)
        {
            Debug.Log("Enemy attack!");
            _isAnimationFinished = true;
        }

        CheckSwitchStates();
    }

    public override void CheckSwitchStates()
    {
        if (_isAnimationFinished)
        {
            SwitchState(Factory.Chase());
        }
    }

    public override void ExitState()
    {
        
    }

    public override void FixedUpdateState()
    {

    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }
}
