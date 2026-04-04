using System;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(DamageInfo info);
}

[Serializable]
public struct DamageInfo
{
    public float DamageAmount;
    public float PoiseDecreaseAmount;
    public Vector3 KnockbackForce;
}
