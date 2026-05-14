using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private EnemyStateMachine stateMachine;
    [SerializeField] private EnemyCombat combat;

    public void EnableHitbox()
    {
        combat.AnimEvent_EnableHitbox();
    }

    public void DisableHitbox()
    {
        combat.AnimEvent_DisableHitbox();
    }

    public void EndAttack()
    {
        if (stateMachine.CurrentState is EnemyAttackState attackState)
            attackState.AnimationFinished();
    }
}
