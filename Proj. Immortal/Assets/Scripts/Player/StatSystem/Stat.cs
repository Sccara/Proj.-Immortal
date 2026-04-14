using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat
{
    public Action<float> OnStatChanged;

    [SerializeField] private float baseValue;
    private List<StatModifier> modifiers = new List<StatModifier>();

    public float BaseValue { get => baseValue; set { baseValue = value; } }
    public float Value
    {
        get
        {
            float finalValue = baseValue;
            float percentAdd = 0;

            foreach (var mod in modifiers)
            {
                if (mod.Type == ModifierType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == ModifierType.PercentAdd)
                {
                    percentAdd += mod.Value;
                }
            }

            return finalValue * (1 + percentAdd);
        }
    }

    public void AddModifier(StatModifier mod)
    {
        modifiers.Add(mod);
        OnStatChanged?.Invoke(Value);
    }

    public void RemoveModifier(StatModifier mod)
    {
        modifiers.Add(mod);
        OnStatChanged?.Invoke(Value);
    }

    public void SetBaseValue(float newValue)
    {
        baseValue = newValue;
        OnStatChanged?.Invoke(Value);
    }
}
