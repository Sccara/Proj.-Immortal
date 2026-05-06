using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : UIWindow
{
    private bool _isDirty;

    [SerializeField] private Inventory inventorySystem;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private ItemSlotUI itemSlotPrefab;

    private List<ItemSlotUI> spawnedSlots = new List<ItemSlotUI>();

    private void OnEnable()
    {
        if (inventorySystem != null)
            inventorySystem.OnInventoryChanged += UpdateInventoryUI;
    }

    private void OnDisable()
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
