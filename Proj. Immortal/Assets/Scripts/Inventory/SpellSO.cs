using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Inventory System/Spell Data")]
public class SpellSO : ItemSO
{
    public float BaseMagicDamage;
    public float ManaCost;

    public SpellEffect SpellPrefab;

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
