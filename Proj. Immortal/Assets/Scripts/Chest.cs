using UnityEditor.UIElements;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemSO item;
    [SerializeField] private int amount = 1;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask defaultLayerMask;

    private bool _isOpen = false;

    public string GetInteractText()
    {
        return _isOpen ? "" : "Open the chest";
    }

    public void Interact(PlayerManager player)
    {
        if (_isOpen)
        {
            return;
        }

        if (animator != null)
        {
            animator.Play("Chest_Open");
        }

        ItemInstance loot;

        if (item is WeaponSO)
        {
            loot = new WeaponInstance(item as WeaponSO);
        }
        else if (item is SpellSO)
        {
            loot = new SpellInstance(item as SpellSO);
        }
        else
        {
            loot = new ItemInstance(item);
        }
        
        player.Inventory.AddItem(loot, amount);

        Debug.Log($"Picked up: {item.itemName} x{amount}");

        _isOpen = true;
        gameObject.layer = LayerMask.NameToLayer("Ground");
    }
}
