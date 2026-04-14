using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [SerializeField] private PlayerConfigSO config;

    [Header("Leveling")]
    [field: SerializeField] public int Level { get; set; } = 1;
    [field: SerializeField] public float CurrentSouls { get; set; }
    [SerializeField] private float baseLevelUpCost = 100f;
    [SerializeField] private float levelCostMultiplier = 1.2f;

    public float LevelUpCost => GetLevelUpCost();

    [Header("Core Attributes")]
    public int Vigor = 10;
    public int Endurance = 10;
    public int Strength = 10;
    public int Dexterity = 10;

    [Header("Stats")]
    public Stat MaxHealthStat = new Stat();
    public Stat MaxStaminaStat = new Stat();
    public Stat AttackPowerStat = new Stat();
    public Stat PoiseAttackPowerStat = new Stat();

    [Header("Hidden Stats")]
    public Stat MoveSpeedStat = new Stat();
    public Stat MaxPoiseStat = new Stat();
    public Stat StaminaRestoreRateStat = new Stat();

    [Header("Resources")]
    public Resource HealthResource;
    public Resource StaminaResource;
    public Resource PoiseResource;

    private void Awake()
    {
        MoveSpeedStat.SetBaseValue(config.walkMoveSpeed);

        StaminaRestoreRateStat.SetBaseValue(config.staminaRestoreRate);

        MaxHealthStat.SetBaseValue(CalculateHPFromVigor(Vigor));
        Debug.Log($"MaxHealth from vigor: {MaxHealthStat.Value}");
        HealthResource = new Resource(MaxHealthStat.Value);
        Debug.Log($"Health: {HealthResource.Current}");
        MaxHealthStat.OnStatChanged += (newMax) => HealthResource.SetMax(newMax, false);

        MaxStaminaStat.SetBaseValue(CalculateStaminaFromEndurance(Endurance));
        StaminaResource = new Resource(MaxStaminaStat.Value);
        MaxStaminaStat.OnStatChanged += (newMax) => StaminaResource.SetMax(newMax, false);

        MaxPoiseStat.SetBaseValue(config.basePoise);
        PoiseResource = new Resource(MaxPoiseStat.Value);
        MaxPoiseStat.OnStatChanged += (newMax) => PoiseResource.SetMax(newMax, false);

        AttackPowerStat.SetBaseValue(CalculateBaseAttack(Strength, Dexterity));
    }

    private float CalculateHPFromVigor(int vigor) => vigor * 10f;
    private float CalculateStaminaFromEndurance(int endurance) => endurance * 5f;
    private float CalculateBaseAttack(int str, int dex) => (str + dex) * 2f;

    private float GetLevelUpCost()
    {
        return Mathf.Round(baseLevelUpCost * Mathf.Pow(Level, levelCostMultiplier));
    }
}
