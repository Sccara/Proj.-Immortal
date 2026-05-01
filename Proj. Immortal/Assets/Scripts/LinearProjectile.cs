using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class LinearProjectile : SpellEffect
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] private GameObject impactParticles;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _rb.isKinematic = true;
    }

    public override void Activate(Transform caster, Transform spawnPoint, DamageInfo finalDamage)
    {
        base.Activate(caster, spawnPoint, finalDamage);

        Destroy(gameObject, lifeTime);
    }

    private void Update() => transform.Translate(Vector3.forward * speed * Time.deltaTime);

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == Caster)
            return;

        if (other.TryGetComponent(out IDamageable target))
            target.TakeDamage(Damage);

        if (impactParticles != null)
        {
            //Instantiate(impactParticles, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
