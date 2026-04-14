using System;
using UnityEngine;

public enum StatType
{
    Vigor,
    Endurance,
    Strength,
    Dexterity
}

public class LevelSystemController : MonoBehaviour
{
    public Action<float> OnSoulsChanged;
    public Action OnLevelUp;

    private void Awake()
    {
        
    }

    private void Start()
    {

    }

    public void AddSouls(float amount)
    {
        PlayerManager.Instance.Attributes.CurrentSouls += amount;
        OnSoulsChanged?.Invoke(PlayerManager.Instance.Attributes.CurrentSouls);
    }

    public void UpgradeStat(StatType statType)
    {
        float cost = PlayerManager.Instance.Attributes.LevelUpCost;

        if (PlayerManager.Instance.Attributes.CurrentSouls >= cost)
        {
            PlayerManager.Instance.Attributes.CurrentSouls -= cost;
            PlayerManager.Instance.Attributes.Level++;

            ApplyStatBonus(statType);

            OnSoulsChanged?.Invoke(PlayerManager.Instance.Attributes.CurrentSouls);
            OnLevelUp?.Invoke();
        }
        else
        {
            Debug.Log("Not enough souls");
        }
    }

    private void ApplyStatBonus(StatType statType)
    {
        switch (statType)
        {
            case StatType.Vigor:
                PlayerManager.Instance.Attributes.Vigor += 1;
                break;
            case StatType.Endurance:
                PlayerManager.Instance.Attributes.Endurance += 1;
                break;
            case StatType.Strength:
                PlayerManager.Instance.Attributes.Strength += 1;
                break;
            case StatType.Dexterity:
                PlayerManager.Instance.Attributes.Dexterity += 1;
                break;
        }
    }
}
