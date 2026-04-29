using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Action OnInventoryChanged;

    [field: SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();

    [SerializeField] private List<ItemSO> items = new List<ItemSO>();
    [SerializeField] private List<WeaponSO> weapons = new List<WeaponSO>();

    public IReadOnlyList<InventorySlot> Slots => slots;

    private void Start()
    {
        foreach (var item in items)
        {
            AddItem(new ItemInstance(item), 5);
        }
        foreach (var weapon in weapons)
        {
            AddItem(new WeaponInstance(weapon));
        }
    }

    public void AddItem(ItemInstance item, int amount = 1)
    {
        if (item.ItemData.isStackable)
        {
            InventorySlot existingSlot = slots.Find(slot => slot.Item != null && slot.Item.ItemData == item.ItemData);

            if (existingSlot != null && existingSlot.Quantity < item.ItemData.maxStackSize)
            {
                existingSlot.AddQuantity(amount);
                OnInventoryChanged?.Invoke();
                return;
            }
        }

        slots.Add(new InventorySlot(item, amount));
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(ItemInstance item, int amount = 1)
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

    public List<InventorySlot> GetAllItemsOfType<T>() where T : ItemInstance
    {
        List<InventorySlot> filteredItems = new List<InventorySlot>();
        foreach (var slot in Slots)
        {
            if (!slot.IsEmpty && slot.Item is T)
            {
                filteredItems.Add(slot);
            }
        }
        return filteredItems;
    }
}
