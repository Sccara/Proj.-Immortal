using System;
using UnityEngine;

[Serializable]
public class HealthSystem 
{
    private float _health;
    private float _maxHealth;

    public float Health => _health;

    public float HealthPercent => _health / _maxHealth;

    public HealthSystem(float health, float maxHealth)
    {
        _health = health;
        _maxHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        _health -= damageAmount;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }

    public void Heal(float healAmount)
    {
        _health += healAmount;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
    }
}
