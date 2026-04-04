using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private float attackStepForce;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    private Rigidbody _rb;
    private InputAction _attackAction;
    private float _nextAttackTime;

    public float KnockbackStrength => knockbackStrength;

    private void Awake()
    {
        _attackAction = InputSystem.actions.FindAction("Attack");
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Time.time >= _nextAttackTime)
        {
            if (_attackAction.WasPressedThisFrame())
            {
                Attack();
                _nextAttackTime = Time.time + PlayerStats.Instance.AttackCooldown;
            }
        }
    }

    private void Attack()
    {
        _rb.AddForce(transform.forward * attackStepForce, ForceMode.Impulse);

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();

            if (damageable != null)
            {
                Vector3 knockbackDirection = (transform.position - enemy.transform.position).normalized;
                DamageInfo info = new DamageInfo() { DamageAmount = PlayerStats.Instance.AttackDamage, KnockbackForce = knockbackDirection * knockbackStrength};

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
