using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private WeaponDamageDetector weaponDetector;

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

    public void ActivateWeapon(float damageMult, float poiseMult, float staminaCost, float stepForce)
    {
        if (GetComponent<StaminaSystemController>().CheckStamina() == false)
            return;

        GetComponent<StaminaSystemController>().UseStamina(staminaCost);
        GetComponent<Rigidbody>().AddForce(transform.forward * stepForce, ForceMode.Impulse);

        // Готовим данные
        float finalDamage = PlayerStats.Instance.AttackDamage * damageMult;
        float finalPoise = PlayerStats.Instance.PoiseDamage * poiseMult;
        Vector3 knockback = transform.forward * (PlayerStats.Instance.KnockbackStrength * damageMult);

        // Включаем меч
        weaponDetector.EnableDamage(finalDamage, finalPoise, knockback);
    }

    public void DeactivateWeapon()
    {
        weaponDetector.DisableDamage();
    }

    public void PerformAttackk(float damageMult, float poiseMult, float staminaCost, float stepForce, float range, float knockback)
    {
        if (!_stamina.CheckStamina()) return;

        _stamina.UseStamina(staminaCost);
        _rb.AddForce(transform.forward * stepForce, ForceMode.Impulse);

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, range, enemyLayers);
        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out IDamageable damageable))
            {
                Vector3 direction = (enemy.transform.position - transform.position).normalized;
                DamageInfo info = new DamageInfo
                {
                    DamageAmount = PlayerStats.Instance.AttackDamage * damageMult,
                    KnockbackForce = direction * (knockback * damageMult),
                    PoiseDecreaseAmount = PlayerStats.Instance.PoiseDamage * poiseMult
                };
                damageable.TakeDamage(info);
            }
        }
    }

    public void StartDamageWindow()
    {
        Debug.Log("StartDamageWindow");
        // Передаем параметры из текущих статов
        weaponDetector.EnableDamage(
            PlayerStats.Instance.AttackDamage,
            PlayerStats.Instance.PoiseDamage,
            transform.forward * PlayerStats.Instance.KnockbackStrength
        );
    }

    public void EndDamageWindow()
    {
        weaponDetector.DisableDamage();
    }

    //private void OnDrawGizmosSelected()
    //{
    //    if (attackPoint == null)
    //        return;

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(attackPoint.position, PlayerStats.Instance.AttackRange);
    //}
}
