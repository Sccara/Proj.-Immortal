using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private float attackStepForce;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;

    private Rigidbody _rb;
    private InputAction _attackAction;
    private float _nextAttackTime;

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
                _nextAttackTime = Time.time + attackCooldown;
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
                Vector3 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                damageable.TakeDamage(attackDamage, knockbackDirection * knockbackStrength);

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
