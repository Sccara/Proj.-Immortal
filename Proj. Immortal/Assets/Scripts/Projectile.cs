using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public DamageInfo DamageInfo;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;

    public float Speed => speed;

    private void Start()
    {
        StartCoroutine(DestroyAfter());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(DamageInfo);
        }

        Destroy(gameObject);
    }

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}
