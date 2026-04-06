using System;
using System.Collections;
using UnityEngine;

public class HealthSystemController : MonoBehaviour, IDamageable
{
    public static Action<float> OnHealthChanged;
    public Action<float> OnPoiseChanged;
    public Action OnPoiseBroken;
    public static Action OnDeath;

    public HealthSystem _healthSystem;

    [SerializeField] private float _currentPoise;
    [SerializeField] private float _timeSinceLastHit;

    private void Start()
    {
        _healthSystem = new HealthSystem(PlayerStats.Instance.Health, PlayerStats.Instance.MaxHealth);
        _currentPoise = PlayerStats.Instance.MaxPoise;

        OnHealthChanged?.Invoke(_healthSystem.HealthPercent);
    }

    private void Update()
    {
        HandlePoiseRegen();
    }

    private void HandlePoiseRegen()
    {
        if (_timeSinceLastHit >= PlayerStats.Instance.PoiseRestoreCooldown && _currentPoise < PlayerStats.Instance.MaxPoise)
        {
            _currentPoise += PlayerStats.Instance.PoiseRestoreMultiplier * Time.deltaTime;
            _currentPoise = Mathf.Min(_currentPoise, PlayerStats.Instance.MaxPoise);
            OnPoiseChanged?.Invoke(_currentPoise / PlayerStats.Instance.MaxPoise);
        }
        _timeSinceLastHit += Time.deltaTime;
    }


    public void TakeDamage(DamageInfo info)
    {
        if (_healthSystem.Health <= 0)
            return;

        _healthSystem.TakeDamage(info.DamageAmount);
        OnHealthChanged?.Invoke(_healthSystem.HealthPercent);

        _timeSinceLastHit = 0;
        PlayerStats.Instance.Poise -= info.PoiseDecreaseAmount; // ???
        _currentPoise -= info.PoiseDecreaseAmount;
        OnPoiseChanged?.Invoke(_currentPoise / PlayerStats.Instance.MaxPoise);

        if (_currentPoise <= 0)
        {
            OnPoiseBroken?.Invoke();
        }

        if (_healthSystem.Health <= 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(float healAmount)
    {
        _healthSystem.Heal(healAmount);
        OnHealthChanged?.Invoke(_healthSystem.HealthPercent);
    }
}
