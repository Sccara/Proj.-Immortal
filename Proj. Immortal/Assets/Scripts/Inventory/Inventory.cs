using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Action OnInventoryChanged;

    [field: SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();
    public IReadOnlyList<InventorySlot> Slots => slots;   

    public void AddItem(ItemInstance item, int amount = 1)
    {
        if (item.ItemData.isStackable)
        {
            InventorySlot existingSlot = slots.Find(slot => slot.Item != null
            && slot.Item.ItemData == item.ItemData
            && slot.Quantity < item.ItemData.maxStackSize);

            if (existingSlot != null)
            {
                int spaceLeft = item.ItemData.maxStackSize - existingSlot.Quantity;

                if (amount <= spaceLeft)
                {
                    existingSlot.AddQuantity(amount);
                    OnInventoryChanged?.Invoke();
                    return;
                }
                else
                {
                    existingSlot.AddQuantity(spaceLeft);
                    amount -= spaceLeft;    
                }


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
