using System.Collections;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] private Transform attackPoint;

    public GameObject expSpherePrefab;

    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;
    private Rigidbody _rb;
    private Transform _player;

    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCooldown;
    [SerializeField] private LayerMask hitLayers;

    private float _lastAttackTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _player = GameObject.Find("Player").transform;
        _enemyMovement = GetComponent<EnemyMovement>();
        _lastAttackTime = 0f;

        _enemyHealth.OnDeath += Death;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _player.position) <= 1.5f && _lastAttackTime <= 0f)
        {
            Attack();
        }

        _lastAttackTime -= Time.deltaTime;
    }

    public void TakeDamage(DamageInfo info)
    {
        _enemyHealth.DecreaseHealth(info.Amount);
        bloodParticle.Play();

        if (_rb != null)
        {
            StartCoroutine(HandleKnockback(info.KnockbackForce));
        }
    }

    private IEnumerator HandleKnockback(Vector3 knockbackForce)
    {
        _enemyMovement.Agent.enabled = false;
        _rb.isKinematic = false;

        _rb.AddForce(knockbackForce, ForceMode.Impulse);

        yield return new WaitForSeconds(0.2f);

        while (_rb.linearVelocity.magnitude > 0.5f)
        {
            yield return null;
        }

        _rb.isKinematic = true;
        _enemyMovement.Agent.enabled = true;

        _enemyMovement.Agent.SetDestination(_player.position);
    }

    public void Attack()
    {
        _lastAttackTime = attackCooldown;

        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRange, hitLayers);

        foreach (Collider collider in hitColliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                DamageInfo info = new DamageInfo() { Amount = attackDamage, KnockbackForce = Vector3.zero };
                damageable.TakeDamage(info);
            }
        }
    }

    public void Death()
    {
        Vector3 randomPosition = transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        Rigidbody sphereRb = Instantiate(expSpherePrefab, randomPosition, Quaternion.identity).GetComponent<Rigidbody>();
        sphereRb.AddForce(Vector3.up, ForceMode.Impulse);
        _enemyHealth.OnDeath -= Death;
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
