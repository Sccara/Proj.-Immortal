using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotUI : ItemSlotUI
{
    [SerializeField] private Button button;
    [SerializeField] private PlayerEquipment equipment;

    public EquipmentSlot HandType;
    public int SlotIndex;

    private void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(OnSlotClicked);
        
    }

    private void OnEnable()
    {
        if (equipment != null)
        {
            equipment.OnEquipmentSlotChanged += UpdateUI;
            RefreshCurrentState();
        }
    }

    private void OnDisable()
    {
        if (equipment != null)
            equipment.OnEquipmentSlotChanged -= UpdateUI;
    }

    public void UpdateUI(EquipmentSlot changedHand, int changedIndex, WeaponInstance newWeapon)
    {
        if (this.HandType == changedHand && this.SlotIndex == changedIndex)
        {
            Init(new InventorySlot(newWeapon));
        }
    }

    private void RefreshCurrentState()
    {
        if (equipment == null)
            return;

        WeaponInstance currentWeaponInThisSlot = (HandType == EquipmentSlot.RightHand)
            ? equipment.RightHandWeapons[SlotIndex]
            : equipment.LeftHandWeapons[SlotIndex];

        Init(new InventorySlot(currentWeaponInThisSlot));
    }

    public void OnSlotClicked()
    {
        var selectionWindow = UIManager.Instance.GetWindow<SelectionWindow>(WindowType.Selection);

        if (selectionWindow != null)
        {
            selectionWindow.OpenForSlot(HandType, SlotIndex);
        }

        UIManager.Instance.ToggleWindow(WindowType.Equipment);
        UIManager.Instance.ToggleWindow(WindowType.Selection, hideHUD: true);
    }
}
