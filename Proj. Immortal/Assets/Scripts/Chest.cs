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

        ItemInstance loot = new ItemInstance(item);
        player.Inventory.AddItem(loot);

        Debug.Log($"Picked up: {item.itemName} x{amount}");

        _isOpen = true;
        gameObject.layer = LayerMask.NameToLayer("Ground");
    }
}
