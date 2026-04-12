using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public ItemType type;
    [TextArea] public string description;
    public bool isStackable;
    public int maxStackSize = 99;
    public int cost;
    public Sprite icon;

    public virtual string AnimationTriggerName => "UseGenericItem";

    public virtual bool Use(PlayerStateMachine player)
    {
        Debug.Log($"Item {itemName} can't be used directly!");
        return false;
    }
}

public enum ItemType
{ 
    Consumable,
    Weapon,
    Material,
    Key
}
