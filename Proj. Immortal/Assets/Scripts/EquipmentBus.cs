using UnityEngine;

public class EquipmentBus : MonoBehaviour
{
    public static EquipmentBus Instance { get; private set; }

    [field: SerializeField] public EquipmentSlotUI SelectedSlot { get; set; }
    [field: SerializeField] public ItemInstance SelectedItem { get; set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
