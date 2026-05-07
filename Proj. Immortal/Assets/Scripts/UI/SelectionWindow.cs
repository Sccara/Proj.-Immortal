using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionWindow : UIWindow
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private List<ItemSlotUI> spawnedSlots;
    [SerializeField] private ItemSlotUI itemSlotPrefab;
    [SerializeField] private Transform content;

    public void OpenForSelection<T>(Action<T> onItemSelected) where T: ItemInstance
    {
        foreach (var slot in spawnedSlots)
        {
            slot.gameObject.SetActive(false);
        }

        List<InventorySlot> items = inventory.GetAllItemsOfType<T>();

        if (items.Count == 0)
            return;

        while (spawnedSlots.Count < items.Count)
        {
            ItemSlotUI newSlotUI = Instantiate(itemSlotPrefab, content);
            spawnedSlots.Add(newSlotUI);
        }

        for (int i = 0; i < items.Count; i++)
        {
            spawnedSlots[i].Init(items[i]);
            spawnedSlots[i].gameObject.SetActive(true);

            Button slotButton = spawnedSlots[i].GetComponent<Button>(); 

            if (slotButton != null)
            {
                slotButton.onClick.RemoveAllListeners();

                T currentItem = items[i].Item as T;

                slotButton.onClick.AddListener(() =>
                {
                    onItemSelected?.Invoke(currentItem);
                    uiManager.ToggleWindow(WindowType.Selection);
                    uiManager.ToggleWindow(WindowType.Equipment, hideHUD: true);
                });
            }
        }
    }

    public override void OnOpen()
    {
        base.OnOpen();
    }

    public void ShowItemsByFilter<T>() where T : ItemInstance
    {
        List<InventorySlot> items = inventory.GetAllItemsOfType<T>();

        while (spawnedSlots.Count < items.Count)
        {
            ItemSlotUI newSlotUI = Instantiate(itemSlotPrefab, content);
            spawnedSlots.Add(newSlotUI);
        }

        for (int i = 0; i < spawnedSlots.Count; i++)
        {
            if (i < items.Count)
            {
                spawnedSlots[i].Init(items[i]);
                spawnedSlots[i].gameObject.SetActive(true);

                Button slotButton = spawnedSlots[i].GetComponent<Button>();
                if (slotButton != null)
                {
                    slotButton.onClick.RemoveAllListeners();

                    ItemInstance currentItem = items[i].Item;
                }
            }
            else
            {
                spawnedSlots[i].gameObject.SetActive(false);
            }
        }
    }

}
