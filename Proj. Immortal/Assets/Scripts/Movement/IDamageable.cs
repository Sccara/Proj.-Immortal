using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(DamageInfo info);
}

public struct DamageInfo
{
    public float Amount;
    public Vector3 KnockbackForce;
}
