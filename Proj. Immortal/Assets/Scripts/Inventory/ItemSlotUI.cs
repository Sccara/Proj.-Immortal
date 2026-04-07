using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemQuantityText;

    public void Init(InventorySlot slot)
    {
        icon.sprite = slot.Item.icon;
        itemNameText.text = slot.Item.itemName;

        if (slot.Item.isStackable && slot.Quantity > 1)
        {
            itemQuantityText.text = slot.Quantity.ToString();
            itemQuantityText.gameObject.SetActive(true);
        }
        else
        {
            itemQuantityText.gameObject.SetActive(false);
        }
    }
}
