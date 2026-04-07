
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Action OnInventoryChanged;

    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();

    [SerializeField] private List<ItemSO> items = new List<ItemSO>();

    public IReadOnlyList<InventorySlot> Slots => slots;

    private void Start()
    {
        foreach (var item in items)
        {
            AddItem(item);
        }
    }

    public void AddItem(ItemSO item, int amount = 1)
    {
        if (item.isStackable)
        {
            InventorySlot existingSlot = slots.Find(slot => slot.Item == item);

            if (existingSlot != null && existingSlot.Quantity < item.maxStackSize)
            {
                existingSlot.AddQuantity(amount);
                OnInventoryChanged?.Invoke();
                return;
            }
        }

        slots.Add(new InventorySlot(item, amount));
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(ItemSO item, int amount = 1)
    {
        InventorySlot existingSlot = slots.Find(slot => slot.Item == item);

        if (existingSlot != null)
        {
            existingSlot.RemoveQuantity(amount);
            if (existingSlot.Quantity <= 0)
            {
                slots.Remove(existingSlot);
            }
            OnInventoryChanged?.Invoke();
        }
    }
}
