using System;

[Serializable]
public class ItemInstance 
{
    public ItemSO ItemData { get; private set; }

    public ItemInstance(ItemSO data)
    {
        ItemData = data;
    }

    public virtual bool Use(PlayerStateMachine player)
    {
        return ItemData.Use(player);
    }
}
