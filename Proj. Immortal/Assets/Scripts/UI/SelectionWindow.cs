using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionWindow : UIWindow
{
    [SerializeField] private PlayerEquipment equipment;
    [SerializeField] private Inventory inventory;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private List<ItemSlotUI> spawnedSlots;
    [SerializeField] private ItemSlotUI itemSlotPrefab;
    [SerializeField] private Transform content;

    private EquipmentSlot _targetHand;
    private int _targetSlotIndex;

    public void OpenForSlot(EquipmentSlot hand, int slotIndex)
    {
        _targetHand = hand;
        _targetSlotIndex = slotIndex;

        ShowItemsByFilter<WeaponInstance>();
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

                    WeaponInstance selectedWeapon = items[i].Item as WeaponInstance;

                    slotButton.onClick.AddListener(() =>
                    {
                        EquipSelectedWeapon(selectedWeapon);
                    });
                }
            }
            else
            {
                spawnedSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private void EquipSelectedWeapon(WeaponInstance weapon)
    {
        equipment.AssignWeaponToSlot(_targetSlotIndex, weapon, _targetHand);

        uiManager.ToggleWindow(WindowType.Selection);
        uiManager.ToggleWindow(WindowType.Equipment, hideHUD : true);
    }
}
