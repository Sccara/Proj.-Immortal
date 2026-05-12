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
}
