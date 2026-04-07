[System.Serializable]
public class InventorySlot
{
    public ItemSO Item { get; private set; }
    public int Quantity { get; private set; }

    public InventorySlot(ItemSO item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public void AddQuantity(int amount) => Quantity += amount;
    public void RemoveQuantity(int amount) => Quantity -= amount;
}