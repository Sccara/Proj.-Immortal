using System;
using UnityEngine;

public class LevelSystemController : MonoBehaviour
{
    public Action<float> OnSoulsChanged;
    public Action OnLevelUp;

    [SerializeField] private SoulsEventChannelSO soulsChannel;
    [SerializeField] private PlayerAttributes attributes;

    private void OnEnable()
    {
        if (soulsChannel != null)
            soulsChannel.OnSoulsDropped += AddSouls;
    }

    private void OnDisable  ()
    {
        if (soulsChannel != null)
            soulsChannel.OnSoulsDropped -= AddSouls;
    }

    public void AddSouls(float amount)
    {
        attributes.CurrentSouls += amount;
        OnSoulsChanged?.Invoke(attributes.CurrentSouls);
    }

    public void UpgradeStat(StatType statType)
    {
        if (attributes.CurrentSouls >= attributes.LevelUpCost)
        {
            attributes.CurrentSouls -= attributes.LevelUpCost;
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
