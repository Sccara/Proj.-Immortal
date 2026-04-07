using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Action OnDeath;

    [SerializeField] private float expReward;

    public Resource Health { get; set; }

    private void Start()
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
        PlayerManager.Instance.Level.AddExp(expReward);

        OnDeath.Invoke();
    }
}
