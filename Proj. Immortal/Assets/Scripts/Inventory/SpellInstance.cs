using UnityEngine;

public class SpellInstance : ItemInstance
{
    public SpellSO SpellData => ItemData as SpellSO;

    public SpellInstance(ItemSO data) : base(data)
    {
    }
}
