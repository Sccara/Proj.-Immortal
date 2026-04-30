using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionWindow : UIWindow
{
    [SerializeField] private PlayerEquipment equipment;
    [SerializeField] private PlayerSpellMemory spellMemory;
    [SerializeField] private Inventory inventory;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private List<ItemSlotUI> spawnedSlots;
    [SerializeField] private ItemSlotUI itemSlotPrefab;
    [SerializeField] private Transform content;

    private EquipmentSlot _targetHand;
    private int _targetSlotIndex;

    private bool _isSelectingSpell = false;
    private int _targetSpellSlotIndex;

    public void OpenForSlot(EquipmentSlot hand, int slotIndex)
    {
        _isSelectingSpell = false;
        _targetHand = hand;
        _targetSlotIndex = slotIndex;

        ShowItemsByFilter<WeaponInstance>();
    }

    public void OpenForSpellSlot(int slotIndex)
    {
        _isSelectingSpell = true;
        _targetSpellSlotIndex = slotIndex;

        ShowItemsByFilter<SpellInstance>();
    }

    public override void OnOpen()
    {
        base.OnOpen();
    }

    public void ShowItemsByFilter<T>() where T : ItemInstance
    {
        List<InventorySlot> items = inventory.GetAllItemsOfType<T>();

        while (spawnedSlots.Count < items.Count)
        {
            ItemSlotUI newSlotUI = Instantiate(itemSlotPrefab, content);
            spawnedSlots.Add(newSlotUI);
        }

        for (int i = 0; i < spawnedSlots.Count; i++)
        {
            if (i < items.Count)
            {
                spawnedSlots[i].Init(items[i]);
                spawnedSlots[i].gameObject.SetActive(true);

                Button slotButton = spawnedSlots[i].GetComponent<Button>();
                if (slotButton != null)
                {
                    slotButton.onClick.RemoveAllListeners();

                    ItemInstance currentItem = items[i].Item;

                    slotButton.onClick.AddListener(() =>
                    {
                        if (_isSelectingSpell && currentItem is SpellInstance spell)
                        {
                            EquipSelectedSpell(spell);
                        }
                        else if (!_isSelectingSpell && currentItem is WeaponInstance weapon)
                        {
                            EquipSelectedWeapon(weapon);
                        }
                    });
                }
            }
            else
            {
                spawnedSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void EquipSelectedWeapon(WeaponInstance weapon)
    {
        equipment.AssignWeaponToSlot(_targetSlotIndex, weapon, _targetHand);

        uiManager.ToggleWindow(WindowType.Selection);
        uiManager.ToggleWindow(WindowType.Equipment, hideHUD : true);
    }

    private void EquipSelectedSpell(SpellInstance spell)
    {
        spellMemory.AssignSpellToSlot(_targetSpellSlotIndex, spell);

        uiManager.ToggleWindow(WindowType.Selection);
        uiManager.ToggleWindow(WindowType.Equipment, hideHUD: true);
    }
}
