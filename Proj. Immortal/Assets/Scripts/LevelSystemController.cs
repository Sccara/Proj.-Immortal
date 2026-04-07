using System;
using UnityEngine;
using UnityEngine.UI;

public enum StatType
{
    Health,
    Stamina,
    AttackDamage
}

public class LevelSystemController : MonoBehaviour
{
    public Action<float> OnSoulsChanged;
    public Action OnLevelUp;

    [Header("Upgrade Values")]
    [SerializeField] private float healthBonusPerLevel = 10f;
    [SerializeField] private float staminaBonusPerLevel = 5f;
    [SerializeField] private float damageBonusPerLevel = 2f;

    [SerializeField] private LevelSystemUI levelSystemUI;
    [SerializeField] private GameObject upgradePanel;
    public LevelSystem _levelSystem;

    private void Awake()
    {
        _levelSystem = new LevelSystem(1.2f, 100);
        _levelSystem.OnLevelUp += ShowUpgradePanel;
    }

    private void Start()
    {
        levelSystemUI.UpdateLevelBar(_levelSystem.ExpPercent, _levelSystem.Level);
    }

    public void AddSouls(float amount)
    {
        PlayerStats.Instance.CurrentSouls += amount;
        OnSoulsChanged?.Invoke(PlayerStats.Instance.CurrentSouls);
    }

    public void UpgradeStat(StatType statType)
    {
        float cost = PlayerStats.Instance.LevelUpCost;

        if (PlayerStats.Instance.CurrentSouls >= cost)
        {
            PlayerStats.Instance.CurrentSouls -= cost;
            PlayerStats.Instance.Level++;

            ApplyStatBonus(statType);

            OnSoulsChanged?.Invoke(PlayerStats.Instance.CurrentSouls);
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
            case StatType.Health:
                float newHealthMax = PlayerStats.Instance.Health.Max + healthBonusPerLevel;
                PlayerStats.Instance.Health.SetMax(newHealthMax);
                break;

            case StatType.Stamina:
                float newStaminaMax = PlayerStats.Instance.Stamina.Max + staminaBonusPerLevel;
                PlayerStats.Instance.Stamina.SetMax(newStaminaMax);
                break;

            case StatType.AttackDamage:
                PlayerStats.Instance.AttackDamage += damageBonusPerLevel;
                break;
        }
    }

    public void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void CloseUpgradePanel()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }
}
