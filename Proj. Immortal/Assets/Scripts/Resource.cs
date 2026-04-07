using System;
using UnityEngine;

[Serializable]
public class Resource
{
    public Action<float> OnValueChanged;

    public float Current { get; set; }
    public float Max { get; private set; }
    public float Percent => Max > 0 ? Current / Max : 0;
    
    public Resource(float max)
    {
        Max = max;
        Current = Max;
    }

    public void Use(float amount)
    {
        Current = Mathf.Clamp(Current - amount, 0, Max);
        OnValueChanged?.Invoke(Percent);
    }

    public void Restore(float amount)
    {
        Current = Mathf.Clamp(Current + amount, 0, Max);
        OnValueChanged?.Invoke(Percent);
    }

    public void SetMax(float newMax, bool resetCurrent = false)
    {
        Max = newMax;
        if (resetCurrent)
        {
            Current = Max;
        }
        OnValueChanged?.Invoke(Percent);
    }
}
