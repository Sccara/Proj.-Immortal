using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIWindow
{
    [SerializeField] private Inventory inventorySystem;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private ItemSlotUI itemSlotPrefab;

    private List<ItemSlotUI> spawnedSlots = new List<ItemSlotUI>();

    private void Awake()
    {
        inventorySystem.OnInventoryChanged += UpdateInventoryUI;
    }

    private void OnDestroy()
    {
        if (inventorySystem != null)
            inventorySystem.OnInventoryChanged -= UpdateInventoryUI;
    }

    public void UpdateInventoryUI()
    {
        var currentSlots = inventorySystem.Slots;

        while (spawnedSlots.Count < currentSlots.Count)
        {
            ItemSlotUI newSlotUI = Instantiate(itemSlotPrefab, inventoryContent);
            spawnedSlots.Add(newSlotUI);
        }

        for (int i = 0; i < spawnedSlots.Count; i++)
        {
            if (i < currentSlots.Count)
            {
                spawnedSlots[i].Init(currentSlots[i]);
                spawnedSlots[i].gameObject.SetActive(true);
            }
            else
            {
                spawnedSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public override void OnOpen()
    {
        base.OnOpen();
        UpdateInventoryUI();
    }
}
