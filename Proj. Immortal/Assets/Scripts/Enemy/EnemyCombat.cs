using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private WeaponDamageDetector weaponDetector;
    [SerializeField] private WeaponSO currentWeaponConfig;
    [SerializeField] private EnemyConfigSO config;

    public WeaponDamageDetector WeaponDetector => weaponDetector;
    public WeaponSO CurrentWeaponConfig => currentWeaponConfig;

    public void AnimEvent_EnableHitbox()
    {
        float finalDamage = CurrentWeaponConfig.BaseDamage;
        float finalPoise = 0f;
        Vector3 knockback = transform.forward * currentWeaponConfig.KnockbackStrength;

        weaponDetector.EnableDamage(finalDamage, finalPoise, knockback);
    }

    public void AnimEvent_DisableHitbox()
    {
        weaponDetector.DisableDamage();
    }
}
