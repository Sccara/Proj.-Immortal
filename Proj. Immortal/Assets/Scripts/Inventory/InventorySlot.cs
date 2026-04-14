using System;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [field: SerializeField] public ItemSO Item { get; private set; }
    [field: SerializeField] public int Quantity { get; private set; }

    public InventorySlot(ItemSO item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public void AddQuantity(int amount) => Quantity += amount;
    public void RemoveQuantity(int amount) => Quantity -= amount;
}