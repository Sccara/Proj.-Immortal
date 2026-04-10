using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private WeaponDamageDetector weaponDetector;

    private float _currentDamageMult = 1f;
    private float _currentPoiseMult = 1f;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    private StaminaSystemController _stamina;

    private Rigidbody _rb;

    public WeaponDamageDetector WeaponDetector => weaponDetector;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _stamina = GetComponent<StaminaSystemController>();
    }

    public void SetAttackMultipliers(float damageMult, float poiseMult)
    {
        _currentDamageMult = damageMult;
        _currentPoiseMult = poiseMult;
    }

    // === ›“» Ã≈“Œƒ€ ¬€«€¬¿ﬁ“—ﬂ »« ANIMATION EVENTS ¬ ”Õ»“» ===

    public void AnimEvent_EnableHitbox()
    {
        float finalDamage = PlayerStats.Instance.AttackDamage * _currentDamageMult;
        float finalPoise = PlayerStats.Instance.PoiseDamage * _currentPoiseMult;
        Vector3 knockback = transform.forward * PlayerStats.Instance.KnockbackStrength * _currentDamageMult;

        weaponDetector.EnableDamage(finalDamage, finalPoise, knockback);
    }

    public void AnimEvent_DisableHitbox()
    {
        weaponDetector.DisableDamage();
    }

    //public void ActivateWeapon(float damageMult, float poiseMult, float staminaCost, float stepForce)
    //{
    //    if (GetComponent<StaminaSystemController>().HasEnoughStamina() == false)
    //        return;

    //    GetComponent<StaminaSystemController>().UseStamina(staminaCost);
    //    GetComponent<Rigidbody>().AddForce(transform.forward * stepForce, ForceMode.Impulse);

    //    float finalDamage = PlayerStats.Instance.AttackDamage * damageMult;
    //    float finalPoise = PlayerStats.Instance.PoiseDamage * poiseMult;
    //    Vector3 knockback = transform.forward * (PlayerStats.Instance.KnockbackStrength * damageMult);

    //    weaponDetector.EnableDamage(finalDamage, finalPoise, knockback);
    //}

    //public void DeactivateWeapon()
    //{
    //    weaponDetector.DisableDamage();
    //}

    //public void PerformAttackk(float damageMult, float poiseMult, float staminaCost, float stepForce, float range, float knockback)
    //{
    //    if (!_stamina.HasEnoughStamina()) return;

    //    _stamina.UseStamina(staminaCost);
    //    _rb.AddForce(transform.forward * stepForce, ForceMode.Impulse);

    //    Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, range, enemyLayers);
    //    foreach (Collider enemy in hitEnemies)
    //    {
    //        if (enemy.TryGetComponent(out IDamageable damageable))
    //        {
    //            Vector3 direction = (enemy.transform.position - transform.position).normalized;
    //            DamageInfo info = new DamageInfo
    //            {
    //                DamageAmount = PlayerStats.Instance.AttackDamage * damageMult,
    //                KnockbackForce = direction * (knockback * damageMult),
    //                PoiseDecreaseAmount = PlayerStats.Instance.PoiseDamage * poiseMult
    //            };
    //            damageable.TakeDamage(info);
    //        }
    //    }
    //}

    //public void StartDamageWindow()
    //{
    //    weaponDetector.EnableDamage(
    //        PlayerStats.Instance.AttackDamage,
    //        PlayerStats.Instance.PoiseDamage,
    //        transform.forward * PlayerStats.Instance.KnockbackStrength
    //    );
    //}

    //public void EndDamageWindow()
    //{
    //    weaponDetector.DisableDamage();
    //}
}
