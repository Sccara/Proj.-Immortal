using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Action<float> OnHealthChanged;
    public Action OnDeath;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public float HealthPercent => health / maxHealth;

    private void Start()
    {
        health = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);

        OnHealthChanged(HealthPercent);

        if (health <= 0)
        {
            Die();
        }
    }

    public void IncreaseHealth(float amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);

        OnHealthChanged(HealthPercent);
    }

    public void Die()
    {
        OnDeath.Invoke();
    }
}
