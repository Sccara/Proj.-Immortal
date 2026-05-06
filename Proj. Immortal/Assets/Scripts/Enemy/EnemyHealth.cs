using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Action OnDeath;
    public Resource Health { get; set; }

    private void Awake()
    {
        Health = new Resource(100);
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
}
