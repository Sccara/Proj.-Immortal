using System;
using UnityEngine;

public class PlayerSpellMemory : MonoBehaviour
{
    public Action<int, SpellInstance> OnSpellSlotChanged;

    public Action<SpellInstance> OnActiveSpellChanged;

    public int MaxMemorySlots = 3;
    public SpellInstance[] MemorizedSpells;

    private int _activeIndex = 0;

    [SerializeField] private InputReader inputReader;

    public SpellInstance CurrentSpell => MemorizedSpells[_activeIndex];

    private void Awake()
    {
        MemorizedSpells = new SpellInstance[MaxMemorySlots];
    }

    private void Start()
    {
        inputReader.OnCycleSpellPressed += CycleSpell;
    }

    private void OnDestroy()
    {
        inputReader.OnCycleSpellPressed -= CycleSpell;
    }

    public void AssignSpellToSlot(int slotIndex, SpellInstance spell)
    {
        if (slotIndex < 0 || slotIndex >= MaxMemorySlots)
            return;

        MemorizedSpells[slotIndex] = spell;

        OnSpellSlotChanged?.Invoke(slotIndex, spell);

        if (_activeIndex == slotIndex)
        {
            OnActiveSpellChanged?.Invoke(spell);
        }
      
    }

    public void CycleSpell()
    {
        Debug.Log("Cycle Spell");

        int nextIndex = _activeIndex;
        bool foundSpell = false;

        for (int i = 0; i < MemorizedSpells.Length; i++)
        {
            nextIndex++;
            if (nextIndex >= MemorizedSpells.Length)
                nextIndex = 0;

            if (MemorizedSpells[nextIndex] != null)
            {
                foundSpell = true;
                break;
            }
       
        }

        if (!foundSpell)
        {
            nextIndex = 0;
        }

        _activeIndex = nextIndex;

        OnActiveSpellChanged?.Invoke(CurrentSpell);
    }
}
