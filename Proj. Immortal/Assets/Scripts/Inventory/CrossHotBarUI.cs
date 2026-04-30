using UnityEngine;
using UnityEngine.UI;

public class CrossHotBarUI : MonoBehaviour
{
    [SerializeField] private PlayerEquipment equipment;
    [SerializeField] private PlayerSpellMemory spellMemory;

    [Header("Icons")]
    [SerializeField] private Image spellIcon;
    [SerializeField] private Image rightWeaponIcon;
    [SerializeField] private Image leftWeaponIcon;

    private void OnEnable()
    {
        if (equipment != null)
            equipment.OnActiveWeaponCycled += UpdateHotbarIcon;

        if (spellMemory != null)
            spellMemory.OnActiveSpellChanged += UpdateSpellIcon;
    }

    private void OnDisable()
    {
        if (equipment != null)
            equipment.OnActiveWeaponCycled -= UpdateHotbarIcon;

        if (spellMemory != null)
            spellMemory.OnActiveSpellChanged -= UpdateSpellIcon;
    }

    private void UpdateHotbarIcon(EquipmentSlot hand, WeaponInstance activeWeapon)
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
}
