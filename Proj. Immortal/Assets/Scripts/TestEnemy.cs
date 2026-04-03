using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private ParticleSystem bloodParticle;
    
    private EnemyHealth _enemyHealth;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    public void TakeDamage(float damage, Vector3 knockbackForce)
    {
        _enemyHealth.DecreaseHealth(damage);
        bloodParticle.Play();

        if (_rb != null)
        {
            _rb.AddForce(knockbackForce, ForceMode.Impulse);
        }
    }
}
