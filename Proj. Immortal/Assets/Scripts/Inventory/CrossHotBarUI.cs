using UnityEngine;
using UnityEngine.UI;

public class CrossHotBarUI : MonoBehaviour
{
    [SerializeField] private PlayerEquipment equipment;
    [SerializeField] private PlayerSpellMemory spellMemory;
    [SerializeField] private PlayerQuickItems quickItems;

    [Header("Icons")]
    [SerializeField] private Image spellIcon;
    [SerializeField] private Image rightWeaponIcon;
    [SerializeField] private Image leftWeaponIcon;
    [SerializeField] private Image quickItemIcon;

    private void OnEnable()
    {
        if (equipment != null)
            equipment.OnActiveWeaponCycled += UpdateWeaponIcon;

        if (spellMemory != null)
            spellMemory.OnActiveSpellChanged += UpdateSpellIcon;

        if (quickItems != null)
            quickItems.OnActiveItemChanged += UpdateQuickItemIcon;
    }

    private void OnDisable()
    {
        if (equipment != null)
            equipment.OnActiveWeaponCycled -= UpdateWeaponIcon;

        if (spellMemory != null)
            spellMemory.OnActiveSpellChanged -= UpdateSpellIcon;

        if (quickItems != null)
            quickItems.OnActiveItemChanged -= UpdateQuickItemIcon;
    }

    private void UpdateWeaponIcon(EquipmentSlot hand, WeaponInstance activeWeapon)
    {
        Image targetIcon = (hand == EquipmentSlot.RightHand) ? rightWeaponIcon : leftWeaponIcon;

        if (activeWeapon == null || activeWeapon.ItemData == null)
        {
            targetIcon.color = new Color(1, 1, 1, 0); 
            targetIcon.sprite = null;
        }
        else
        {
            targetIcon.color = new Color(1, 1, 1, 1); 
            targetIcon.sprite = activeWeapon.ItemData.icon;
        }
    }

    public void UpdateSpellIcon(SpellInstance activeSpell)
    {
        if (activeSpell == null || activeSpell.ItemData == null)
        {
            spellIcon.color = new Color(1, 1, 1, 0);
            spellIcon.sprite = null;
        }
        else
        {
            spellIcon.color = new Color(1, 1, 1, 1);
            spellIcon.sprite = activeSpell.ItemData.icon;
        }
    }

    public void UpdateQuickItemIcon(ItemInstance item)
    {
        if (item == null || item.ItemData == null)
        {
            spellIcon.color = new Color(1, 1, 1, 0);
            spellIcon.sprite = null;
        }
        else
        {
            spellIcon.color = new Color(1, 1, 1, 1);
            spellIcon.sprite = item.ItemData.icon;
        }
    }
}
