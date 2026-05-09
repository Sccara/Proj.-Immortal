using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine _stateMachine;
    [SerializeField] private PlayerCombat _combat;

    [SerializeField] private Transform magicSpawnPoint;
    public void EnableHitbox()
    {
        _combat.AnimEvent_EnableHitbox();
    }

    public void DisableHitbox()
    {
        _combat.AnimEvent_DisableHitbox();
    }

    public void EndAttack()
    {
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
        _stateMachine.PlayerManager.QuickItems.ConsumeCurrentItem(_stateMachine);
    }

    public void AnimEvent_FireSpell()
    {
        Debug.Log("Fire spell!");
        SpellInstance activeSpell = _stateMachine.PlayerManager.SpellMemory.CurrentSpell;
        if (activeSpell == null)
            return;

        SpellSO spellData = activeSpell.SpellData;
        float manaCost = activeSpell.SpellData.ManaCost;

     
        if (_stateMachine.PlayerManager.Mana.HasEnoughMana(manaCost))
        {
            _stateMachine.PlayerManager.Mana.UseMana(manaCost);

            DamageInfo finalDamage = new DamageInfo { DamageAmount = spellData.BaseMagicDamage };

            if (spellData.SpellPrefab != null)
            {
                SpellEffect spawnedSpell = Instantiate(spellData.SpellPrefab);

                spawnedSpell.Activate(_stateMachine.transform, magicSpawnPoint, finalDamage);
            }
            else
            {
                Debug.LogWarning("Spell has no prefab assigned");
            }
        }
    }
}
