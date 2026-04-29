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
        if (slot.Item == null || slot.Item.ItemData == null)
        {
            icon.gameObject.SetActive(false);
            itemNameText.text = "";
            itemQuantityText.gameObject.SetActive(false);
            return;
        }

        ItemSO data = slot.Item.ItemData;

        icon.gameObject.SetActive(true);
        icon.sprite = data.icon;

        if (itemNameText != null)
            itemNameText.text = data.itemName;
       
        if (data.isStackable && slot.Quantity > 1)
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
