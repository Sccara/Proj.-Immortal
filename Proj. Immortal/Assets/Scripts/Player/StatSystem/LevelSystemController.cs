using System;
using UnityEngine;

public class LevelSystemController : MonoBehaviour
{
    public Action<float> OnSoulsChanged;
    public Action OnLevelUp;

    public void AddSouls(float amount)
    {
        PlayerManager.Instance.Attributes.CurrentSouls += amount;
        OnSoulsChanged?.Invoke(PlayerManager.Instance.Attributes.CurrentSouls);
    }

    public void UpgradeStat(StatType statType)
    {
        var attributes = PlayerManager.Instance.Attributes;
        float cost = attributes.LevelUpCost;

        if (attributes.CurrentSouls >= cost)
        {
            attributes.CurrentSouls -= cost;
            attributes.Level++;
            attributes.UpgradeAttribute(statType);

            OnSoulsChanged?.Invoke(attributes.CurrentSouls);
            OnLevelUp?.Invoke();
        }
        else
        {
            Debug.Log("Not enough souls");
        }
    }
}
