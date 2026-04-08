using TMPro;
using UnityEngine;

public class LevelUpWindow : UIWindow
{
    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI currentSoulsText;
    [SerializeField] private TextMeshProUGUI nextLevelCostText;
    [SerializeField] private TextMeshProUGUI currentLevelText;

    [Header("Stat Texts")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI damageText;

    private void OnEnable()
    {
        // ѕодписываемс€ на обновлени€
        PlayerManager.Instance.Level.OnLevelUp += RefreshUI;
        PlayerManager.Instance.Level.OnSoulsChanged += UpdateSoulsText;

        RefreshUI(); // ќбновл€ем текст при открытии панели
    }

    private void OnDisable()
    {
        if (PlayerManager.Instance != null)
        {
            PlayerManager.Instance.Level.OnLevelUp -= RefreshUI;
            PlayerManager.Instance.Level.OnSoulsChanged -= UpdateSoulsText;
        }
    }

    private void RefreshUI()
    {
        UpdateSoulsText(PlayerStats.Instance.CurrentSouls);
        nextLevelCostText.text = $"Cost: {PlayerStats.Instance.LevelUpCost}";
        currentLevelText.text = $"Level: {PlayerStats.Instance.Level}";

        // ѕоказываем текущие значени€ статов
        healthText.text = $"Health: {PlayerStats.Instance.Health.Max}";
        staminaText.text = $"Stamina: {PlayerStats.Instance.Stamina.Max}";
        damageText.text = $"Damage: {PlayerStats.Instance.AttackDamage}";
    }

    private void UpdateSoulsText(float currentSouls)
    {
        currentSoulsText.text = $"Souls: {currentSouls}";
    }

    // Ёти методы нужно назначить на событи€ OnClick() у кнопок в Unity Editor
    public void OnUpgradeHealthClicked() => PlayerManager.Instance.Level.UpgradeStat(StatType.Health);
    public void OnUpgradeStaminaClicked() => PlayerManager.Instance.Level.UpgradeStat(StatType.Stamina);
    public void OnUpgradeDamageClicked() => PlayerManager.Instance.Level.UpgradeStat(StatType.AttackDamage);

    public void OnCloseClicked()
    {
        PlayerManager.Instance.Level.CloseUpgradePanel();
        gameObject.SetActive(false);
    }

    public override void OnOpen()
    {
        base.OnOpen();
        RefreshUI();
    }
}
