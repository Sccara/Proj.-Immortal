using TMPro;
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

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI quickItemQuantityText;

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
            quickItemIcon.color = new Color(1, 1, 1, 0);
            quickItemIcon.sprite = null;
            quickItemQuantityText.text = "";
        }
        else
        {
            int quantity = quickItems.GetCurrentItemQuantity();

            quickItemIcon.sprite = item.ItemData.icon;
            quickItemQuantityText.text = quantity.ToString();

            if (quantity <= 0)
            {
                quickItemIcon.color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
                quickItemQuantityText.color = Color.red;
            }
            else
            {
                quickItemIcon.color = Color.white;
                quickItemQuantityText.color = Color.white;
            }
        }
    }
}
