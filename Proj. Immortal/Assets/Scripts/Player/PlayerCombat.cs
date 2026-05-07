using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private WeaponDamageDetector weaponDetector;
    [SerializeField] private WeaponSO currentWeaponConfig;
    [SerializeField] private PlayerAttributes attributes;

    private float _currentDamageMult = 1f;
    private float _currentPoiseMult = 1f;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    public WeaponDamageDetector WeaponDetector => weaponDetector;
    public WeaponSO CurrentWeaponConfig => currentWeaponConfig;

    public void SetAttackMultipliers(float damageMult, float poiseMult)
    {
        _currentDamageMult = damageMult;
        _currentPoiseMult = poiseMult;
    }


    public void AnimEvent_EnableHitbox()
    {
        float finalDamage = attributes.RightHandAttackStat.Value * _currentDamageMult; // + LEFT HAND
        float finalPoise = attributes.PoiseAttackPowerStat.Value * _currentPoiseMult;
        Vector3 knockback = transform.forward * currentWeaponConfig.KnockbackStrength * _currentDamageMult;

        weaponDetector.EnableDamage(finalDamage, finalPoise, knockback);
    }

    public void AnimEvent_DisableHitbox()
    {
        weaponDetector.DisableDamage();
    }
}
