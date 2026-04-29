using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private QuickItemsSystem quickItemsSystem;

    [Header("UI Elements (Down Slot)")]
    [SerializeField] private GameObject itemSlotContainer;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI quantityText;

    private void Awake()
    {
        quickItemsSystem.OnActiveItemChanged += UpdateHotbarUI;
    }

    private void Start()
    {
        UpdateHotbarUI();
    }

    private void OnDestroy()
    {
        if (quickItemsSystem != null)
            quickItemsSystem.OnActiveItemChanged -= UpdateHotbarUI;
    }

    private void UpdateHotbarUI()
    {
        ItemInstance currentItem = quickItemsSystem.GetCurrentItem();

        if (currentItem == null)
        {
            itemSlotContainer.SetActive(false);
            return;
        }

        itemSlotContainer.SetActive(true);
        itemIcon.sprite = currentItem.ItemData.icon;

        int currentQuantity = quickItemsSystem.GetCurrentItemQuantity();

        quantityText.text = currentQuantity.ToString();

        itemIcon.color = currentQuantity > 0 ? Color.white : new Color(1, 1, 1, 0.3f);
    }
}
