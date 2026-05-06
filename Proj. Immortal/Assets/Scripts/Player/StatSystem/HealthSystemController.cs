using System;
using System.Collections;
using UnityEngine;

public class HealthSystemController : MonoBehaviour, IDamageable
{
    public static Action OnDeath;

    [SerializeField] private PlayerAttributes attributes;
    [SerializeField] private PlayerConfigSO config;

    [field: SerializeField] public bool IsInvulnerable { get; set; } = false;

    public void TakeDamage(DamageInfo info)
    {
        if (attributes.HealthResource.Current <= 0 || IsInvulnerable)
            return;

        attributes.HealthResource.Use(info.DamageAmount);

        if (attributes.HealthResource.Current <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(float healAmount)
    {
        attributes.HealthResource.Restore(healAmount);
    }

    public void TriggerInvulnerability(float duration)
    {
        StartCoroutine(InvulnerabilityCoroutine(duration));
    }

    private IEnumerator InvulnerabilityCoroutine(float duration)
    {
        IsInvulnerable = true;
        yield return new WaitForSeconds(duration);
        IsInvulnerable = false;
    }
}
