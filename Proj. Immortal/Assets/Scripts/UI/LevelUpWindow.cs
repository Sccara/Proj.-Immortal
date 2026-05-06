using TMPro;
using UnityEngine;

public class LevelUpWindow : UIWindow
{
    [Header("Player References")]
    [SerializeField] private LevelSystemController playerLevel;
    [SerializeField] private PlayerAttributes playerAttributes;

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
        playerLevel.OnLevelUp += RefreshUI;
        playerLevel.OnSoulsChanged += UpdateSoulsText;

        RefreshUI(); // ќбновл€ем текст при открытии панели
    }

    private void OnDisable()
    {
        if (playerLevel != null)
        {
            playerLevel.OnLevelUp -= RefreshUI;
            playerLevel.OnSoulsChanged -= UpdateSoulsText;
        }
    }

    private void RefreshUI()
    {
        UpdateSoulsText(playerAttributes.CurrentSouls);
        nextLevelCostText.text = $"Cost: {playerAttributes.LevelUpCost}";
        currentLevelText.text = $"Level: {playerAttributes.Level}";

        // ѕоказываем текущие значени€ статов
        vigorText.text = $"Vigor: {playerAttributes.Vigor}";
        enduranceText.text = $"Endurance: {playerAttributes.Endurance}";
        strengthText.text = $"Strength: {playerAttributes.Strength}";
        dexterityText.text = $"Dexterity: {playerAttributes.Dexterity}";
        mindText.text = $"Mind: {playerAttributes.Mind}";
    }

    private void UpdateSoulsText(float currentSouls)
    {
        currentSoulsText.text = $"Souls: {currentSouls}";
    }

    // Ёти методы нужно назначить на событи€ OnClick() у кнопок в Unity Editor
    public void OnUpgradeVigorClicked() => playerLevel.UpgradeStat(StatType.Vigor);
    public void OnUpgradeEnduranceClicked() => playerLevel.UpgradeStat(StatType.Endurance);
    public void OnUpgradeStrengthClicked() => playerLevel.UpgradeStat(StatType.Strength);
    public void OnUpgradeDexterityClicked() => playerLevel.UpgradeStat(StatType.Dexterity);
    public void OnUpgradeMindClicked() => playerLevel.UpgradeStat(StatType.Mind);

    public override void OnOpen()
    {
        base.OnOpen();
        RefreshUI();
    }
}
