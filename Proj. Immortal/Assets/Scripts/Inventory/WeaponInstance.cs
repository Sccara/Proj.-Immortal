using System;
using UnityEngine;

[Serializable]
public class WeaponInstance : ItemInstance
{
    [field: SerializeField] public int UpgradeLevel { get; set; }
    public WeaponSO WeaponData => ItemData as WeaponSO;

    public WeaponInstance(WeaponSO data) : base(data)
    {
        UpgradeLevel = 0;
    }
    public float GetCurrentBaseDamage()
    {
        if (WeaponData == null)
            return 0;

        return WeaponData.BaseDamage + (UpgradeLevel * WeaponData.DamageUpgradePerLevel);
    }

}
