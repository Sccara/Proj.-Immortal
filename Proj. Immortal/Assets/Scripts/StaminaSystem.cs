using System;
using UnityEngine;

[Serializable]
public class StaminaSystem
{
    private float _stamina;
    private float _maxStamina;
    private bool _isExhausted;

    private float _staminaRestoreMultiplier = 1f;
    private float _staminaExhaustedMultiplier = 0.5f;

    public float Stamina => _stamina;

    public bool IsExhausted => _isExhausted;

    public float StaminaRestoreMultiplier => _staminaRestoreMultiplier;

    public float StaminaPercent => _stamina / _maxStamina;

    public StaminaSystem(float stamina, float maxStamina)
    {
        _stamina = stamina;
        _maxStamina = maxStamina;
        _staminaExhaustedMultiplier = 0.5f;
    }

    public void UseStamina(float amount)
    {
        _stamina -= amount;
        if (_stamina <= 0)
        {
            _isExhausted = true;
        }
        _stamina = Mathf.Clamp(_stamina, 0, _maxStamina);
    }

    public void RestoreStamina(float amount)
    {
        if (_isExhausted)
        {
            _stamina += (amount * _staminaRestoreMultiplier) * _staminaExhaustedMultiplier;
        }
        else
        {
            _stamina += amount * _staminaRestoreMultiplier;
        }
        
        _stamina = Mathf.Clamp(_stamina, 0, _maxStamina);
        if (_stamina >= _maxStamina)
        {
            _isExhausted = false;
        }
    }
}
