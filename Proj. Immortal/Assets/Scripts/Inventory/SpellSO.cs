using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Inventory System/Spell")]
public class SpellSO : ItemSO
{
    public float manaCost;
    public GameObject projectilePrefab;
    public float spellDamage;
    public float castTime; // Время до вылета снаряда

    public override string AnimationTriggerName => "CastSpell";

    public override bool Use(PlayerStateMachine player)
    {
        Debug.Log("Can't use spell directly!");
        return false;
    }

    public void Cast(PlayerAttributes attributes, Transform castPoint, Transform targetLock)
    {

    }
}
