using System;
using UnityEngine;
using System.Collections.Generic;

public class Equipment : MonoBehaviour
{
    public Action<EquipmentSlot, WeaponInstance, int> OnWeaponChanged;

    public List<WeaponInstance> RightHandWeapons = new List<WeaponInstance>();
    public List<WeaponInstance> LeftHandWeapons = new List<WeaponInstance>();

    private int _activeRightIndex = 0;
    private int _activeLeftIndex = 0;

    public WeaponInstance CurrentRightWeapon => RightHandWeapons[_activeRightIndex];
    public WeaponInstance CurrentLeftWeapon => LeftHandWeapons[_activeLeftIndex];

    public void SetWeaponToSlot(WeaponInstance weapon, EquipmentSlot hand, int slotIndex)
    {
        if (slotIndex < 0 || slotIndex > 2)
            return;

        if (hand == EquipmentSlot.RightHand)
        {
            RightHandWeapons[slotIndex] = weapon;
        }
        else
        {
            LeftHandWeapons[slotIndex] = weapon;
        }
    }
}
