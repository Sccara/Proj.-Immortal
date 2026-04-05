using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private float attackStepForce;
    [SerializeField] private float _chargeStartTime;
    [SerializeField] private bool _isCharging;
    [SerializeField] private float maxChargeTime;
    [SerializeField] private float attackStamina;
    [SerializeField] private float heavyAttackStamina;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    [SerializeField] private float minDamageMultiplier;
    [SerializeField] private float maxDamageMultiplier;
    [SerializeField] private float maxPoiseMultiplier;

    private Rigidbody _rb;
    private InputAction _attackAction;
    private InputAction _heavyAttackAction;
    private float _nextAttackTime;
    public float KnockbackStrength => knockbackStrength;

    private void Awake()
    {
        _attackAction = InputSystem.actions.FindAction("Attack");
        _heavyAttackAction = InputSystem.actions.FindAction("HeavyAttack");
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _heavyAttackAction.started += OnHeavyAttackStarted;
        _heavyAttackAction.canceled += OnHeavyAttackReleased;
    }

    private void OnDisable()
    {
        _heavyAttackAction.started -= OnHeavyAttackStarted;
        _heavyAttackAction.canceled -= OnHeavyAttackReleased;
    }

    private void Update()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (_attackAction.WasPressedThisFrame())
            {
                Attack(attackStamina);
                _nextAttackTime = Time.time + PlayerStats.Instance.AttackCooldown;
            }
        }
    }

    private void OnHeavyAttackStarted(InputAction.CallbackContext context)
    {
        _chargeStartTime = (float)context.startTime;
        _isCharging = true;
    }

    private void OnHeavyAttackReleased(InputAction.CallbackContext context)
    {
        if (!_isCharging) 
            return;

        float holdDuration = (float)context.time - _chargeStartTime;
        float chargePercent = Mathf.Clamp01(holdDuration / maxChargeTime);

        Attack(heavyAttackStamina, chargePercent);

        _isCharging = false;
    }

    public void CancelAttack()
    {
        _isCharging = false;
    }

    private void Attack(float stamina, float chargePercent = 0)
    {
        if (GetComponent<StaminaSystemController>().CheckStamina() == false)
            return;

        float damageMult;
        float poiseMult;
        if (chargePercent != 0)
        {
            damageMult = Mathf.Lerp(minDamageMultiplier, maxDamageMultiplier, chargePercent);
            poiseMult = Mathf.Lerp(1f, maxPoiseMultiplier, chargePercent);
        }
        else
        {
            damageMult = 1;
            poiseMult = 1;
        }

        _rb.AddForce(transform.forward * attackStepForce, ForceMode.Impulse);
        GetComponent<StaminaSystemController>().UseStamina(stamina);

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();

            if (damageable != null)
            {
                Vector3 knockbackDirection = (transform.position - enemy.transform.position).normalized;

                DamageInfo info = new DamageInfo()
                { 
                    DamageAmount = PlayerStats.Instance.AttackDamage * damageMult,
                    KnockbackForce = knockbackDirection * (knockbackStrength * damageMult),
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
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
