using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory System/Weapon")]
public class WeaponSO : ItemSO
{
    [Header("Weapon Stats")]
    public float BaseDamage = 50f;
    public float BasePoiseDamage = 10f;
    public float KnockbackStrength = 5f;
    public float DamageUpgradePerLevel = 2f;

    [Header("Scaling")]
    public float StrengthScaling = 0.5f; // Бонус от силы
    public float DexterityScaling = 0.1f; // Бонус от ловкости
    public float MindScaling = 0.1f; // Бонус от ловкости

    [Header("Heavy Attack Logic")]
    public float MaxChargeTime = 2f;
    public float MinDamageMultiplier = 1.0f;
    public float MaxDamageMultiplier = 2.5f;
    public float MaxPoiseMultiplier = 2.0f;
    public float AttackStepForce = 15f; // Насколько сильно персонаж шагает вперед при ударе этим оружием
}
