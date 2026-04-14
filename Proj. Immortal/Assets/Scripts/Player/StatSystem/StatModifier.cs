using UnityEngine;

public class StatModifier
{
    public float Value;
    public ModifierType Type;
    public object Source;
}

public enum ModifierType
{
    Flat,
    PercentAdd
}
