using UnityEngine;
using UnityEngine.UI;

public class QuickItemSlotUI : ItemSlotUI
{
    [SerializeField] private Button button;
    [SerializeField] private PlayerQuickItems quickItems;

    [Header("Slot Identity")]
    public int SlotIndex;

    private void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(OnSlotClicked);
    }

    private void OnEnable()
    {
        if (quickItems != null)
            quickItems.OnItemSlotChanged += UpdateUI;

        RefreshCurrentState();
    }

    private void OnDisable()
    {
        if (quickItems != null)
            quickItems.OnItemSlotChanged -= UpdateUI;
    }

    private void UpdateUI(int changedIndex, ItemInstance newItem)
    {
        if (SlotIndex == changedIndex)
        {
            Init(new InventorySlot(newItem));
        }
    }

    private void RefreshCurrentState()
    {
        if (quickItems == null)
            return;

        Init(new InventorySlot(quickItems.EquippedItems[SlotIndex]));
    }

    private void OnSlotClicked()
    {
        var selectionWindow = UIManager.Instance.GetWindow<SelectionWindow>(WindowType.Selection);
        if (selectionWindow != null)
        {
            selectionWindow.OpenForSelection<ItemInstance>(item =>
            {
                quickItems.AssignItemToSlot(SlotIndex, item);
            });
        }

        UIManager.Instance.ToggleWindow(WindowType.Equipment);
        UIManager.Instance.ToggleWindow(WindowType.Selection, hideHUD: true);
    }
}
