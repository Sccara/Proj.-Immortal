using UnityEngine;
using UnityEngine.UI;

public class CrossHotBarUI : MonoBehaviour
{
    [SerializeField] private PlayerEquipment equipment;

    [Header("Иконки")]
    [SerializeField] private Image rightWeaponIcon;
    [SerializeField] private Image leftWeaponIcon;

    private void OnEnable()
    {
        if (equipment != null)
            equipment.OnActiveWeaponCycled += UpdateHotbarIcon;
    }

    private void OnDisable()
    {
        if (equipment != null)
            equipment.OnActiveWeaponCycled -= UpdateHotbarIcon;
    }

    private void UpdateHotbarIcon(EquipmentSlot hand, WeaponInstance activeWeapon)
    {
        Image targetIcon = (hand == EquipmentSlot.RightHand) ? rightWeaponIcon : leftWeaponIcon;

        if (activeWeapon == null || activeWeapon.ItemData == null)
        {
            // Голые кулаки
            targetIcon.color = new Color(1, 1, 1, 0); // Прозрачная
            targetIcon.sprite = null;
        }
        else
        {
            // В руках оружие
            targetIcon.color = new Color(1, 1, 1, 1); // Видимая
            targetIcon.sprite = activeWeapon.ItemData.icon;
        }
    }
}
