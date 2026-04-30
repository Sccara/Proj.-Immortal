using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine _stateMachine;
    [SerializeField] private PlayerCombat _combat;

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
        else if (_stateMachine.CurrentState is PlayerCastState castState)
            castState.AnimationFinished();
    }

    public void OnItemUsed()
    {
        if (_stateMachine.CurrentState is PlayerUseItemState useItemState)
        {
            useItemState.AnimationFinished();
        }
    }

    public void AnimEvent_ApplyItemEffect()
    {
        // Обращаемся к QuickItemsSystem и просим применить предмет
        _stateMachine.PlayerManager.QuickItems.ConsumeCurrentItem(_stateMachine);
    }

    public void AnimEvent_FireSpell()
    {
        SpellInstance activeSpell = _stateMachine.PlayerManager.SpellMemory.CurrentSpell;
        if (activeSpell == null) return;

        float manaCost = activeSpell.SpellData.manaCost;

     
        if (_stateMachine.PlayerManager.Attributes.ManaResource.Current >= manaCost)
        {
            _stateMachine.PlayerManager.Attributes.ManaResource.Use(manaCost);

            // Здесь мы будем спавнить префаб! 
            // К этому мы перейдем в 5-м блоке (Механика Эффектов).
            Debug.Log($"КАСТ! Списано {manaCost} маны. Вылетел спелл: {activeSpell.ItemData.itemName}");
        }
    }
}
