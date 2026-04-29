using System.Collections.Generic;
using UnityEngine;
using System;

public class SpellSlotSystem : MonoBehaviour
{
    public Action OnActiveSpellChanged;

    [SerializeField] private InputReader inputReader;
    [SerializeField] private Inventory inventorySystem;
    [SerializeField] private int maxSpellSlots;

    [SerializeField] private List<SpellInstance> memorizedSpells = new List<SpellInstance>();
    private int _currentIndex = 0;

    private void Start()
    {
        inventorySystem.OnInventoryChanged += NotifyUI;
        inputReader.OnCycleSpellPressed += CycleNextSpell;
    }

    private void OnDestroy()
    {
        if (inventorySystem != null)
            inventorySystem.OnInventoryChanged -= NotifyUI;
    }

    public SpellInstance GetCurrentSpell()
    {
        if (memorizedSpells.Count == 0)
            return null;

        return memorizedSpells[_currentIndex];
    }

    public void CycleNextSpell()
    {
        if (memorizedSpells.Count <= 1)
        {
            return;
        }

        _currentIndex++;

        if (_currentIndex >= memorizedSpells.Count)
        {
            _currentIndex = 0;
        }

        NotifyUI();
    }

    public void EquipSpell(SpellInstance item)
    {
        if (item.ItemData.type != ItemType.Spell)
            return;

        if (!memorizedSpells.Contains(item) && memorizedSpells.Count < maxSpellSlots)
        {
            memorizedSpells.Add(item);
            NotifyUI();
        }
    }

    public void UseCurrentSpell(PlayerStateMachine player)
    {
        SpellInstance currentSpell = GetCurrentSpell();

        if (currentSpell != null)
        {
            bool success = currentSpell.Use(player);

            if (success)
            {
                inventorySystem.RemoveItem(currentSpell);
                NotifyUI();
            }
        }
    }

    private void NotifyUI() => OnActiveSpellChanged?.Invoke();
}
