using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public Action<EquipmentSlot, int, WeaponInstance> OnEquipmentSlotChanged;

    public Action<EquipmentSlot, WeaponInstance> OnActiveWeaponCycled;
    public WeaponInstance[] RightHandWeapons = new WeaponInstance[3];
    public WeaponInstance[] LeftHandWeapons = new WeaponInstance[3];

    [SerializeField] private InputReader inputReader;
    [SerializeField] private Inventory inventorySystem;

    private int _currentRightHandIndex = 0;
    private int _currentLeftHandIndex = 0;

    public WeaponInstance RightHand => GetWeapon(EquipmentSlot.RightHand);
    public WeaponInstance LeftHand => GetWeapon(EquipmentSlot.LeftHand);

    [Header("ЗАДЕЛ НА БУДУЩЕЕ (Пока оставляем пустым)")]
    [SerializeField] private Transform rightHandSocket; // Кость правой руки в скелете
    // private GameObject _currentWeaponModel;

    private void Start()
    {
        inputReader.OnCycleRightHandWeaponPressed += CycleRightWeapon;
        inputReader.OnCycleLeftHandWeaponPressed += CycleLeftWeapon;
    }

    public void AssignWeaponToSlot(int slotIndex, WeaponInstance weapon, EquipmentSlot slot)
    {
        if (slot == EquipmentSlot.RightHand)
        {
            RightHandWeapons[slotIndex] = weapon;
            OnEquipmentSlotChanged?.Invoke(slot, slotIndex, weapon);
        }
        if (slot == EquipmentSlot.LeftHand)
        {
            LeftHandWeapons[slotIndex] = weapon;
            OnEquipmentSlotChanged?.Invoke(slot, slotIndex, weapon);
        }
    }

    public void CycleRightWeapon()
    {
        _currentRightHandIndex++;

        if (_currentRightHandIndex >= RightHandWeapons.Length)
        {
            _currentRightHandIndex = 0;
        }

        OnActiveWeaponCycled?.Invoke(EquipmentSlot.RightHand, RightHandWeapons[_currentRightHandIndex]);
    }

    public void CycleLeftWeapon()
    {
        _currentLeftHandIndex++;

        if (_currentLeftHandIndex >= LeftHandWeapons.Length)
        {
            _currentLeftHandIndex = 0;
        }

        OnActiveWeaponCycled?.Invoke(EquipmentSlot.LeftHand, LeftHandWeapons[_currentLeftHandIndex]);
    }

    public WeaponInstance GetWeapon(EquipmentSlot slot)
    {
        // REFACTOR TO FIST DEFAULT WEAPON

        if (slot == EquipmentSlot.RightHand && RightHandWeapons[_currentRightHandIndex] == null)
        {
            return null;
        }
        else if (slot == EquipmentSlot.LeftHand && RightHandWeapons[_currentLeftHandIndex] == null)
        {
            return null;
        }

        return slot == EquipmentSlot.RightHand ? RightHandWeapons[_currentRightHandIndex] : LeftHandWeapons[_currentLeftHandIndex];
    }
}

public enum EquipmentSlot
{
    RightHand,
    LeftHand,
    Spell,
    QuickItem
}
