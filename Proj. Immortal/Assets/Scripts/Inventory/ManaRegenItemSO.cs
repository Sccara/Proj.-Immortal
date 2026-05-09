using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Mana Item")]
public class ManaRegenItemSO : ItemSO
{
    [Header("Heal Settings")]
    public float manaRegenAmount;

    public override string AnimationTriggerName => "DrinkFlask";

    public override bool Use(PlayerStateMachine player)
    {
        if (player.PlayerManager.Attributes.ManaResource.Current >= player.PlayerManager.Attributes.ManaResource.Max)
        {
            Debug.Log("Mana is max!");
            return false;
        }

        player.PlayerManager.Mana.RestoreMana(manaRegenAmount);

        return true;
    }
}
