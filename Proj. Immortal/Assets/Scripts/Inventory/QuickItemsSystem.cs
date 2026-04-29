using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuickItemsSystem : MonoBehaviour
{
    [Header("TEST")]
    public HealItemSO item;

    public Action OnActiveItemChanged;

    [SerializeField] private InputReader inputReader;
    [SerializeField] private Inventory inventorySystem;
    [SerializeField] private int maxQuickSlots;

    [SerializeField] private List<ItemInstance> equippedItems = new List<ItemInstance>();
    private int _currentIndex = 0;

    private void Start()
    {
        inventorySystem.OnInventoryChanged += NotifyUI;
        inputReader.OnCycleQuickItemPressed += CycleNextItem;

        equippedItems.Add(new ItemInstance(item));

        NotifyUI();
    }

    private void OnDestroy()
    {
        if (inventorySystem != null)
            inventorySystem.OnInventoryChanged -= NotifyUI;
    }

    public ItemInstance GetCurrentItem()
    {
        if (equippedItems.Count == 0)
            return null;
        
        return equippedItems[_currentIndex];
    }

    public int GetCurrentItemQuantity()
    {
        ItemInstance current = GetCurrentItem();

        if (current == null) 
            return 0;

        foreach (var slot in inventorySystem.Slots)
        {
            if (slot.Item == current)
            {
                return slot.Quantity;
            }
        }

        return 0;
    }

    public void CycleNextItem()
    {
        if (equippedItems.Count <= 1)
        {
            return;
        }

        _currentIndex++;

        if (_currentIndex >= equippedItems.Count)
        {
            _currentIndex = 0;
        }

        NotifyUI();
    }

    public void EquipItem(ItemInstance item)
    {
        if (item.ItemData.type != ItemType.Consumable)
            return;

        if (!equippedItems.Contains(item) && equippedItems.Count < maxQuickSlots)
        {
            equippedItems.Add(item);
            NotifyUI();
        }
    }

    public void ConsumeCurrentItem(PlayerStateMachine player)
    {
        ItemInstance currentItem = GetCurrentItem();
        int quantity = GetCurrentItemQuantity();

        if (currentItem != null && quantity > 0)
        {
            bool success = currentItem.Use(player);

            if (success)
            {
                inventorySystem.RemoveItem(currentItem, 1);
                NotifyUI();
            }
        }
    }

    private void NotifyUI() => OnActiveItemChanged?.Invoke();
}
