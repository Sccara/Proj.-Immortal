using UnityEngine;
using UnityEngine.UI;

public class SpellSlotUI : ItemSlotUI
{
    [SerializeField] private Button button;
    [SerializeField] private PlayerSpellMemory spellMemory; 

    [Header("Slot Identity")]
    public int SlotIndex; 

    private void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(OnSlotClicked);
    }

    private void OnEnable()
    {
        if (spellMemory != null)
            spellMemory.OnSpellSlotChanged += UpdateUI;

        RefreshCurrentState();
    }

    private void OnDisable()
    {
        if (spellMemory != null)
            spellMemory.OnSpellSlotChanged -= UpdateUI;
    }

    private void UpdateUI(int changedIndex, SpellInstance newSpell)
    {
        if (this.SlotIndex == changedIndex)
        {
            Init(new InventorySlot(newSpell));
        }
    }

    private void RefreshCurrentState()
    {
        if (spellMemory == null) 
            return;

        Init(new InventorySlot(spellMemory.MemorizedSpells[SlotIndex]));
    }

    private void OnSlotClicked()
    {
        var selectionWindow = UIManager.Instance.GetWindow<SelectionWindow>(WindowType.Selection);
        if (selectionWindow != null)
        {
            selectionWindow.OpenForSpellSlot(SlotIndex);
        }

        UIManager.Instance.ToggleWindow(WindowType.Equipment);
        UIManager.Instance.ToggleWindow(WindowType.Selection, hideHUD: true);
    }
}
