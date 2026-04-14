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
    [SerializeField] protected float timeSinceLastHit;
    [SerializeField] protected float poiseRestoreCooldown;
    [SerializeField] protected float poiseRestoreMultiplier;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float poiseDamage;
    [SerializeField] protected float poise;
    [SerializeField] protected float maxPoise;
    [SerializeField] protected bool isStaggered;
    [SerializeField] protected float staggeredTime;
    [SerializeField] protected LayerMask hitLayers;
    
    [SerializeField] protected float lastAttackTime;

    protected virtual void Awake()
    {
        health = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        health.OnDeath += Death;

        lastAttackTime = attackCooldown;
        poise = maxPoise;
    }

    private void Start()
    {
        player = PlayerManager.Instance.transform;
    }


    protected virtual void Update()
    {
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            ExecuteAttack();
        }
        else
        {
            MoveToPlayer();
        }

        if (timeSinceLastHit >= poiseRestoreCooldown && poise <= maxPoise)
        {
            poise += poiseRestoreMultiplier * Time.deltaTime;
            poise = Mathf.Clamp(poise, 0, maxPoise);
        }

        lastAttackTime -= Time.deltaTime;
        timeSinceLastHit += Time.deltaTime;
    }

    protected void ExecuteAttack()
    {
        if (lastAttackTime > 0 || isStaggered == true)
            return;
        
        Attack();
        lastAttackTime = attackCooldown;
    }

    public abstract void Attack();

    public void TakeDamage(DamageInfo info)
    {
        health.DecreaseHealth(info.DamageAmount);
        DecreasePoise(info.PoiseDecreaseAmount);
        bloodParticle.Play();
        timeSinceLastHit = 0f;

        if (rb != null)
        {
            StartCoroutine(HandleKnockback(info.KnockbackForce));
        }
    }

    private void DecreasePoise(float amount)
    {
        if (isStaggered)
            return;

        poise -= amount;
        CheckStagger();
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
        if (!isStaggered)
        {
            agent.enabled = true;
            agent.SetDestination(player.position);
        } 
    }

    protected void CheckStagger()
    {
        if (poise <= 0)
        {
            poise = 0;
            StartCoroutine(Stagger());
        }
    }

    protected IEnumerator Stagger()
    {
        agent.enabled = false;
        isStaggered = true;
        Color oldColor = GetComponent<MeshRenderer>().material.color;
        GetComponent<MeshRenderer>().material.color = Color.black;

        yield return new WaitForSeconds(staggeredTime);

        GetComponent<MeshRenderer>().material.color = oldColor;
        agent.enabled = true;
        agent.SetDestination(player.position);
        isStaggered = false;
        poise = maxPoise;
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
