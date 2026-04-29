using System;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [field: SerializeField] public ItemInstance Item { get; private set; }
    [field: SerializeField] public int Quantity { get; private set; }

    public bool IsEmpty { get; private set; }

    public InventorySlot()
    {
        Item = null;
        Quantity = 0;
        IsEmpty = true;
    }

    public InventorySlot(ItemInstance item)
    {
        Item = item;
        Quantity = 1;
        IsEmpty = false;
    }

    public InventorySlot(ItemInstance item, int quantity)
    {
        Item = item;
        Quantity = quantity;
        IsEmpty = false;
    }

    public void AddQuantity(int amount) => Quantity += amount;
    public void RemoveQuantity(int amount) => Quantity -= amount;
}