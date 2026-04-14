using UnityEngine;
using UnityEngine.UI;

public class EquipmentUI : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private PlayerEquipment equipmentSystem;

    [Header("Right Hand Slot")]
    [SerializeField] private GameObject rightSlotContainer;
    [SerializeField] private Image rightWeaponIcon;

    [Header("Left Hand Slot")]
    [SerializeField] private GameObject leftSlotContainer;
    [SerializeField] private Image leftWeaponIcon;

    private void Awake()
    {
        equipmentSystem.OnWeaponEquipped += UpdateWeaponUI;
    }

    private void Start()
    {
        UpdateWeaponUI(EquipmentSlot.RightHand, equipmentSystem.RightWeapon);
        UpdateWeaponUI(EquipmentSlot.LeftHand, equipmentSystem.LeftWeapon);
    }

    private void OnDestroy()
    {
        if (equipmentSystem != null)
            equipmentSystem.OnWeaponEquipped -= UpdateWeaponUI;
    }

    private void UpdateWeaponUI(EquipmentSlot slot, WeaponSO weapon)
    {
        GameObject currentContainer = slot == EquipmentSlot.RightHand ? rightSlotContainer : leftSlotContainer;
        Image currentIcon = slot == EquipmentSlot.RightHand ? rightWeaponIcon : leftWeaponIcon;

        if (weapon == null)
        {
            currentContainer.SetActive(false);
            return;
        }

        currentContainer.SetActive(true);
        currentIcon.sprite = weapon.icon;
    }
}
