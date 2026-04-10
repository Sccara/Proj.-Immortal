using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine _stateMachine;
    [SerializeField] private PlayerCombat _combat;

    // Вызывать на кадре, где меч начинает наносить урон
    public void EnableHitbox() => _combat.AnimEvent_EnableHitbox();

    // Вызывать на кадре, где замах закончился
    public void DisableHitbox() => _combat.AnimEvent_DisableHitbox();

    // Вызывать в самом КОНЦЕ анимации удара
    public void EndAttack()
    {
        // Передаем сигнал стейт машине, что атака завершена
        if (_stateMachine.CurrentState is PlayerLightAttackState lightState)
            lightState.AnimationFinished();
        else if (_stateMachine.CurrentState is PlayerHeavyAttackState heavyState)
            heavyState.AnimationFinished();
    }
}
