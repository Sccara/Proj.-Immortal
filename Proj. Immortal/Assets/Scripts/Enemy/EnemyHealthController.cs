using System;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyConfigSO config;

    public Action OnDeath;
    public Action<DamageInfo> OnTakeHit;
    public Resource Health { get; set; }

    private void Awake()
    {
        Health = new Resource(config.Health);
    }

    public void DecreaseHealth(float amount)
    {
        Health.Use(amount);

        if (Health.Current <= 0)
        {
            Die();
        }
    }

    public void IncreaseHealth(float amount)
    {
        Health.Restore(amount);
    }

    public void Die()
    {
        OnDeath?.Invoke();
    }

    public void TakeDamage(DamageInfo info)
    {
        OnTakeHit?.Invoke(info);
        DecreaseHealth(info.DamageAmount);
        Debug.Log($"Enemy take damage, health: {Health.Current}");
    }
}
