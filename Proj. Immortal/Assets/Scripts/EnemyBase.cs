using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    protected EnemyHealth health;
    protected NavMeshAgent agent;
    protected Rigidbody rb;
    protected Transform player;

    [SerializeField] private ParticleSystem bloodParticle;
    [SerializeField] protected GameObject expSpherePrefab;
    [SerializeField] protected Transform attackPoint;

    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected LayerMask hitLayers;


    [SerializeField] protected float lastAttackTime;

    protected virtual void Awake()
    {
        health = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        health.OnDeath += Death;

        lastAttackTime = attackCooldown;
    }

    private void Start()
    {
        player = PlayerStats.Instance.transform;
    }


    protected virtual void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            ExecuteAttack();
        }
        else
        {
            MoveToPlayer();
        }

        lastAttackTime -= Time.deltaTime;
    }

    protected void ExecuteAttack()
    {
        if (lastAttackTime > 0)
            return;
        
        Attack();
        lastAttackTime = attackCooldown;
    }

    public abstract void Attack();

    public void TakeDamage(DamageInfo info)
    {
        health.DecreaseHealth(info.Amount);
        bloodParticle.Play();

        if (rb != null)
        {
            StartCoroutine(HandleKnockback(info.KnockbackForce));
        }
    }

    private IEnumerator HandleKnockback(Vector3 knockbackForce)
    {
        agent.enabled = false;
        rb.isKinematic = false;

        rb.AddForce(knockbackForce, ForceMode.Impulse);

        yield return new WaitForSeconds(0.2f);

        while (rb.linearVelocity.magnitude > 0.5f)
        {
            yield return null;
        }

        rb.isKinematic = true;
        agent.enabled = true;

        agent.SetDestination(player.position);
    }

    protected void MoveToPlayer()
    {
        if (agent.enabled)
            agent.SetDestination(player.position);
    }

    private void Death()
    {
        Vector3 randomPosition = transform.position + new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        Rigidbody sphereRb = Instantiate(expSpherePrefab, randomPosition, Quaternion.identity).GetComponent<Rigidbody>();
        sphereRb.AddForce(Vector3.up, ForceMode.Impulse);
        health.OnDeath -= Death;
        Destroy(gameObject);
    }
}
