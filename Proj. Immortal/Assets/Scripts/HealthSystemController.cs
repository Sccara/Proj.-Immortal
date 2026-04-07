using System;
using System.Collections;
using UnityEngine;

public class HealthSystemController : MonoBehaviour, IDamageable
{
    public Action OnPoiseBroken;
    public static Action OnDeath;

    [SerializeField] private float _timeSinceLastHit;
    private PlayerStats _playerStats;

    [field: SerializeField] public bool IsInvulnerable { get; set; } = false;
    public bool CanBeStaggered { get; set; } = true;
    

    private void Start()
    {
        _playerStats = PlayerStats.Instance;
    }

    private void Update()
    {
        HandlePoiseRegen();
    }

    private void HandlePoiseRegen()
    {
        if (_timeSinceLastHit >= _playerStats.PoiseRestoreCooldown && _playerStats.Poise.Current < _playerStats.Poise.Max)
        {
            _playerStats.Poise.Restore(_playerStats.PoiseRestoreMultiplier * Time.deltaTime);
        }
        _timeSinceLastHit += Time.deltaTime;
    }

    public void TakeDamage(DamageInfo info)
    {
        if (_playerStats.Health.Current <= 0 || IsInvulnerable)
            return;

        _playerStats.Health.Use(info.DamageAmount);

        _timeSinceLastHit = 0;

        if (CanBeStaggered)
        {
            _playerStats.Poise.Use(info.PoiseDecreaseAmount);

            if (_playerStats.Poise.Current <= 0)
            {
                OnPoiseBroken?.Invoke();
            }
        }

        if (_playerStats.Health.Current <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(float healAmount)
    {
        _playerStats.Health.Restore(healAmount);
    }

    public void ResetPoise()
    {
        _playerStats.Poise.Current = _playerStats.Poise.Max;
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
