using UnityEngine;
using UnityEngine.Rendering;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private WeaponDamageDetector weaponDetector;
    [SerializeField] private WeaponSO currentWeaponConfig;

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

    // === ›“» Ã≈“Œƒ€ ¬€«€¬¿ﬁ“—ﬂ »« ANIMATION EVENTS ¬ UNITY ===

    public void AnimEvent_EnableHitbox()
    {
        float finalDamage = PlayerManager.Instance.Attributes.RightHandAttackStat.Value * _currentDamageMult; // + LEFT HAND
        float finalPoise = PlayerManager.Instance.Attributes.PoiseAttackPowerStat.Value * _currentPoiseMult;
        Vector3 knockback = transform.forward * currentWeaponConfig.KnockbackStrength * _currentDamageMult;

        weaponDetector.EnableDamage(finalDamage, finalPoise, knockback);
    }

    public void AnimEvent_DisableHitbox()
    {
        weaponDetector.DisableDamage();
    }
}
