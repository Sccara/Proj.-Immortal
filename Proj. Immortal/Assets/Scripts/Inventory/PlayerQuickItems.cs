using System;
using UnityEngine;

public class PlayerQuickItems : MonoBehaviour
{
    public Action<ItemInstance> OnActiveItemChanged;
    public Action<int, ItemInstance> OnItemSlotChanged;

    [SerializeField] private InputReader inputReader;
    [SerializeField] private Inventory inventorySystem;

    public int MaxQuickSlots = 5;
    public ItemInstance[] EquippedItems;

    private int _currentIndex = 0;

    public ItemInstance CurrentItem => EquippedItems[_currentIndex];

    private void Awake()
    {
        EquippedItems = new ItemInstance[MaxQuickSlots];
    }

    private void Start()
    {
        if (inventorySystem != null)
            inventorySystem.OnInventoryChanged += NotifyUI;

        if (inputReader != null)
            inputReader.OnCycleQuickItemPressed += CycleNextItem;

        NotifyUI();
    }

    private void OnDestroy()
    {
        if (inventorySystem != null)
            inventorySystem.OnInventoryChanged -= NotifyUI;

        if (inputReader != null)
            inputReader.OnCycleQuickItemPressed -= CycleNextItem;
    }

    public int GetCurrentItemQuantity()
    {
        ItemInstance current = CurrentItem;

        if (current == null) 
            return 0;

        foreach (var slot in inventorySystem.Slots)
        {
            if (slot.Item == current)
            {
                return slot.Quantity;
            }
        }

        return 0;
    }

    public void CycleNextItem()
    {
        Debug.Log("Cycle Item");

        int nextIndex = _currentIndex;
        bool foundItem = false;

        for (int i = 0; i < EquippedItems.Length; i++)
        {
            nextIndex++;
            if (nextIndex >= EquippedItems.Length)
                nextIndex = 0;

            if (EquippedItems[nextIndex] != null)
            {
                foundItem = true;
                break;
            }

        }

        if (!foundItem)
        {
            nextIndex = 0;
        }

        _currentIndex = nextIndex;

        NotifyUI();
    }

    public void AssignItemToSlot(int slotIndex, ItemInstance item)
    {
        if (slotIndex < 0 || slotIndex >= MaxQuickSlots)
            return;

        Debug.Log($"Type 1: {EquippedItems[slotIndex]}");
        Debug.Log($"Type 2: {item}");

        EquippedItems[slotIndex] = item;

        OnItemSlotChanged?.Invoke(slotIndex, item);

        if (_currentIndex == slotIndex)
        {
            NotifyUI();
        }
    }

    public void ConsumeCurrentItem(PlayerStateMachine player)
    {
        ItemInstance currentItem = CurrentItem;
        int quantity = GetCurrentItemQuantity();

        if (currentItem == null)
            return;

        if (quantity > 0)
        {
            bool success = currentItem.Use(player);

            if (success)
            {
                inventorySystem.RemoveItem(currentItem, 1);
                NotifyUI();
            }
        }
    }

    private void NotifyUI()
    {
        OnActiveItemChanged?.Invoke(CurrentItem);
    }

}
