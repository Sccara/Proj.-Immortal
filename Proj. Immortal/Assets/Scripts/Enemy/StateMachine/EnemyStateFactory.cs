using UnityEngine;

public class EnemyStateFactory : MonoBehaviour
{
    EnemyStateMachine _context;

    public EnemyStateFactory(EnemyStateMachine currentContext)
    {
        _context = currentContext;
    }

    public EnemyBaseState Idle()
    {
        return new EnemyIdleState(_context, this);
    }

    public EnemyChaseState Chase()
    {
        return new EnemyChaseState(_context, this);
    }

    public EnemyAttackState Attack()
    {
        return new EnemyAttackState(_context, this);
    }

    public EnemyPatrolState Patrol()
    {
        return new EnemyPatrolState(_context, this);
    }

    public EnemyDeadState Dead()
    {
        return new EnemyDeadState(_context, this);
    }

    public EnemyStrafeState Strafe()
    {
        return new EnemyStrafeState(_context, this);
    }

    public EnemyStunState Stun()
    {
        return new EnemyStunState(_context, this);
    }

    public EnemyImpactState Impact(Vector3 _knockback)
    {
        return new EnemyImpactState(_context, this, _knockback);
    }
}
