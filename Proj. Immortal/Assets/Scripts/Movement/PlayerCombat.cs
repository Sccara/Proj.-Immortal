using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    private StaminaSystemController _stamina;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _stamina = GetComponent<StaminaSystemController>();
    }

    //public void PerformAttack(float stamina, float chargePercent = 0)
    //{
    //    if (GetComponent<StaminaSystemController>().CheckStamina() == false)
    //        return;

    //    float damageMult;
    //    float poiseMult;
    //    if (chargePercent != 0)
    //    {
    //        damageMult = Mathf.Lerp(minDamageMultiplier, maxDamageMultiplier, chargePercent);
    //        poiseMult = Mathf.Lerp(1f, maxPoiseMultiplier, chargePercent);
    //    }
    //    else
    //    {
    //        damageMult = 1;
    //        poiseMult = 1;
    //    }

    //    _rb.AddForce(transform.forward * attackStepForce, ForceMode.Impulse);
    //    GetComponent<StaminaSystemController>().UseStamina(stamina);

    //    Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

    //    foreach (Collider enemy in hitEnemies)
    //    {
    //        IDamageable damageable = enemy.GetComponent<IDamageable>();

    //        if (damageable != null)
    //        {
    //            Vector3 knockbackDirection = (transform.position - enemy.transform.position).normalized;

    //            DamageInfo info = new DamageInfo()
    //            { 
    //                DamageAmount = PlayerStats.Instance.AttackDamage * damageMult,
    //                KnockbackForce = knockbackDirection * (knockbackStrength * damageMult),
    //                PoiseDecreaseAmount = PlayerStats.Instance.PoiseDamage * poiseMult
    //            };

    //            damageable.TakeDamage(info);
    //        }
    //    }
    //}

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

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, PlayerStats.Instance.AttackRange);
    }
}
