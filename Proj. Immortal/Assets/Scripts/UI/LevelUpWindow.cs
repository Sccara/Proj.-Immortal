using TMPro;
using UnityEngine;

public class LevelUpWindow : UIWindow
{
    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI currentSoulsText;
    [SerializeField] private TextMeshProUGUI nextLevelCostText;
    [SerializeField] private TextMeshProUGUI currentLevelText;

    [Header("Stat Texts")]
    [SerializeField] private TextMeshProUGUI vigorText;
    [SerializeField] private TextMeshProUGUI enduranceText;
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI dexterityText;
    [SerializeField] private TextMeshProUGUI mindText;

    private void OnEnable()
    {
        // ѕодписываемс€ на обновлени€
        PlayerManager.Instance.Level.OnLevelUp += RefreshUI;
        PlayerManager.Instance.Level.OnSoulsChanged += UpdateSoulsText;

        RefreshUI(); // ќбновл€ем текст при открытии панели
    }

    private void OnDisable()
    {
        if (PlayerManager.Instance.Level != null)
        {
            PlayerManager.Instance.Level.OnLevelUp -= RefreshUI;
            PlayerManager.Instance.Level.OnSoulsChanged -= UpdateSoulsText;
        }
    }

    private void RefreshUI()
    {
        UpdateSoulsText(PlayerManager.Instance.Attributes.CurrentSouls);
        nextLevelCostText.text = $"Cost: {PlayerManager.Instance.Attributes.LevelUpCost}";
        currentLevelText.text = $"Level: {PlayerManager.Instance.Attributes.Level}";

        // ѕоказываем текущие значени€ статов
        vigorText.text = $"Vigor: {PlayerManager.Instance.Attributes.Vigor}";
        enduranceText.text = $"Endurance: {PlayerManager.Instance.Attributes.Endurance}";
        strengthText.text = $"Strength: {PlayerManager.Instance.Attributes.Strength}";
        dexterityText.text = $"Dexterity: {PlayerManager.Instance.Attributes.Dexterity}";
        mindText.text = $"Mind: {PlayerManager.Instance.Attributes.Mind}";
    }

    private void UpdateSoulsText(float currentSouls)
    {
        currentSoulsText.text = $"Souls: {currentSouls}";
    }

    // Ёти методы нужно назначить на событи€ OnClick() у кнопок в Unity Editor
    public void OnUpgradeVigorClicked() => PlayerManager.Instance.Level.UpgradeStat(StatType.Vigor);
    public void OnUpgradeEnduranceClicked() => PlayerManager.Instance.Level.UpgradeStat(StatType.Endurance);
    public void OnUpgradeStrengthClicked() => PlayerManager.Instance.Level.UpgradeStat(StatType.Strength);
    public void OnUpgradeDexterityClicked() => PlayerManager.Instance.Level.UpgradeStat(StatType.Dexterity);
    public void OnUpgradeMindClicked() => PlayerManager.Instance.Level.UpgradeStat(StatType.Mind);

    public override void OnOpen()
    {
        base.OnOpen();
        RefreshUI();
    }
}
