using UnityEngine;

public abstract class SpellEffect : MonoBehaviour
{
    public abstract void Initialize(DamageInfo info, Transform caster, Transform targetLock);
}
