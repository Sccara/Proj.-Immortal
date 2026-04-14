using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Heal Item")]
public class HealItemSO : ItemSO
{
    [Header("Heal Settings")]
    public float healAmount;

    public override string AnimationTriggerName => "DrinkFlask";

    public override bool Use(PlayerStateMachine player)
    {
        if (player.PlayerManager.Attributes.HealthResource.Current >= player.PlayerManager.Attributes.HealthResource.Max)
        {
            Debug.Log("Health is max!");
            return false;
        }

        player.PlayerManager.Health.Heal(healAmount);

        return true;
    }
}
