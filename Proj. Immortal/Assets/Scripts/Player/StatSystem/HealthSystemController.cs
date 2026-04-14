using System;
using System.Collections;
using UnityEngine;

public class HealthSystemController : MonoBehaviour, IDamageable
{
    public Action OnPoiseBroken;
    public static Action OnDeath;

    [SerializeField] private float _timeSinceLastHit;
    private PlayerAttributes _attributes;

    [field: SerializeField] public bool IsInvulnerable { get; set; } = false;
    public bool CanBeStaggered { get; set; } = true;
    

    private void Start()
    {
        _attributes = PlayerManager.Instance.Attributes;
    }

    private void Update()
    {
        HandlePoiseRegen();
    }

    private void HandlePoiseRegen()
    {
        if (_timeSinceLastHit >= PlayerManager.Instance.Config.poiseRestoreCooldown && _attributes.PoiseResource.Current < _attributes.PoiseResource.Max)
        {
            _attributes.PoiseResource.Restore(PlayerManager.Instance.Config.poiseRestoreMultiplier * Time.deltaTime);
        }
        _timeSinceLastHit += Time.deltaTime;
    }

    public void TakeDamage(DamageInfo info)
    {
        if (_attributes.HealthResource.Current <= 0 || IsInvulnerable)
            return;

        _attributes.HealthResource.Use(info.DamageAmount);

        _timeSinceLastHit = 0;

        if (CanBeStaggered)
        {
            _attributes.PoiseResource.Use(info.PoiseDecreaseAmount);

            if (_attributes.PoiseResource.Current <= 0)
            {
                OnPoiseBroken?.Invoke();
            }
        }

        if (_attributes.HealthResource.Current <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(float healAmount)
    {
        _attributes.HealthResource.Restore(healAmount);
    }

    public void ResetPoise()
    {
        _attributes.PoiseResource.Current = _attributes.PoiseResource.Max;
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
