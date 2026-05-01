using UnityEngine;

public abstract class SpellEffect : MonoBehaviour
{
    protected DamageInfo Damage;
    protected Transform Caster;

    public virtual void Activate(Transform caster, Transform spawnPoint, DamageInfo finalDamage)
    {
        Caster = caster;
        Damage = finalDamage;

        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;
    }
}
