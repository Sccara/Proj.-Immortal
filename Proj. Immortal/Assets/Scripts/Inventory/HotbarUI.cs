using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarUI : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private QuickItemsSystem quickItemsSystem;

    [Header("UI Elements (Down Slot)")]
    [SerializeField] private GameObject itemSlotContainer; // Весь слот (чтобы скрывать, если пусто)
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI quantityText;

    private void Awake()
    {
        quickItemsSystem.OnActiveItemChanged += UpdateHotbarUI;
    }

    private void Start()
    {
        UpdateHotbarUI(); // Инициализация при старте
    }

    private void OnDestroy()
    {
        if (quickItemsSystem != null)
            quickItemsSystem.OnActiveItemChanged -= UpdateHotbarUI;
    }

    private void UpdateHotbarUI()
    {
        ItemSO currentItem = quickItemsSystem.GetCurrentItem();

        if (currentItem == null)
        {
            // Если ничего не экипировано — скрываем слот
            itemSlotContainer.SetActive(false);
            return;
        }

        // Включаем слот и настраиваем визуал
        itemSlotContainer.SetActive(true);
        itemIcon.sprite = currentItem.icon;

        int currentQuantity = quickItemsSystem.GetCurrentItemQuantity();

        // Как в Souls: даже если зелий 0, иконка остается, просто показывает 0
        quantityText.text = currentQuantity.ToString();

        // Опционально: Делаем иконку полупрозрачной, если предметов не осталось
        itemIcon.color = currentQuantity > 0 ? Color.white : new Color(1, 1, 1, 0.3f);
    }
}
