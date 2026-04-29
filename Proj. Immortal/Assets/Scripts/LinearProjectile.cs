using UnityEngine;

public class LinearProjectile : SpellEffect
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifeTime = 5f;
    private DamageInfo _damageInfo;

    public override void Initialize(DamageInfo info, Transform caster, Transform targetLock)
    {
        _damageInfo = info;
        Destroy(gameObject, lifeTime);

        // ≈сли есть таргет лок, поворачиваемс€ к нему
        if (targetLock != null)
            transform.LookAt(targetLock.position + Vector3.up);
    }

    private void Update() => transform.Translate(Vector3.forward * speed * Time.deltaTime);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable target))
            target.TakeDamage(_damageInfo);

        Destroy(gameObject);
    }
}
