using System;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public Action<EquipmentSlot, WeaponSO> OnWeaponEquipped;

    [field: SerializeField] public WeaponSO RightWeapon { get; private set; }
    [field: SerializeField] public WeaponSO LeftWeapon { get; private set; }

    [Header("ЗАДЕЛ НА БУДУЩЕЕ (Пока оставляем пустым)")]
    [SerializeField] private Transform rightHandSocket; // Кость правой руки в скелете
    // private GameObject _currentWeaponModel;

    private void Start()
    {
        if (RightWeapon != null)
            EquipWeapon(RightWeapon, EquipmentSlot.RightHand);

        if (LeftWeapon != null)
            EquipWeapon(LeftWeapon, EquipmentSlot.LeftHand);
    }

    public void EquipWeapon(WeaponSO newWeapon, EquipmentSlot slot)
    {
        if (slot == EquipmentSlot.RightHand)
        {
            RightWeapon = newWeapon;
        }
        else if (slot == EquipmentSlot.LeftHand)
        {
            LeftWeapon = newWeapon;
        }

        /* ТУТ БУДЕТ ЛОГИКА ДЛЯ МОДЕЛЕЙ:
        if (_currentWeaponModel != null) Destroy(_currentWeaponModel);
        if (newWeapon.WeaponPrefab != null && rightHandSocket != null)
        {
            _currentWeaponModel = Instantiate(newWeapon.WeaponPrefab, rightHandSocket);
        }
        */

        // Оповещаем систему статов, что пушка сменилась
        OnWeaponEquipped?.Invoke(slot, newWeapon);

        string weaponName = newWeapon != null ? newWeapon.itemName : "Кулаки";
        Debug.Log($"<color=orange>Экипировано [{weaponName}] в слот {slot}</color>");
    }

    // Временный метод для теста (повесим на кнопку мыши или клавиатуры)
    public void CycleRightWeaponTest(WeaponSO nextWeapon)
    {
        EquipWeapon(nextWeapon, EquipmentSlot.RightHand);
    }

    public void CycleLeftWeaponTest(WeaponSO nextWeapon)
    {
        EquipWeapon(nextWeapon, EquipmentSlot.LeftHand);
    }

    public WeaponSO GetWeapon(EquipmentSlot slot)
    {
        return slot == EquipmentSlot.RightHand ? RightWeapon : LeftWeapon;
    }
}

public enum EquipmentSlot
{
    RightHand,
    LeftHand
}
