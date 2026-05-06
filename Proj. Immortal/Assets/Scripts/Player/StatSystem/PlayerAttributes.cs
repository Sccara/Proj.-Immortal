using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    [SerializeField] private PlayerConfigSO config;
    [SerializeField] private PlayerEquipment equipment;

    [Header("Leveling")]
    [field: SerializeField] public int Level { get; set; } = 1;
    [field: SerializeField] public float CurrentSouls { get; set; }
    [SerializeField] private float baseLevelUpCost = 100f;
    [SerializeField] private float levelCostMultiplier = 1.2f;

    public float LevelUpCost => GetLevelUpCost();

    [Header("Core Attributes")]
    [field: SerializeField] public int Vigor { get; private set; } = 10;
    [field: SerializeField] public int Endurance { get; private set; } = 10;
    [field: SerializeField] public int Strength { get; private set; } = 10;
    [field: SerializeField] public int Dexterity { get; private set; } = 10;
    [field: SerializeField] public int Mind { get; private set; } = 10;

    [Header("Stats")]
    public Stat MaxHealthStat = new Stat();
    public Stat MaxStaminaStat = new Stat();
    public Stat MaxManaStat = new Stat();
    public Stat RightHandAttackStat = new Stat();
    public Stat LeftHandAttackStat = new Stat();
    public Stat PoiseAttackPowerStat = new Stat();

    [Header("Hidden Stats")]
    public Stat MoveSpeedStat = new Stat();
    public Stat StaminaRestoreRateStat = new Stat();

    [Header("Resources")]
    public Resource HealthResource;
    public Resource StaminaResource;
    public Resource ManaResource;

    [Header("DEBUG")]
    public string health;
    public string stamina;
    public string mana;
    public string leftHand;
    public string rightHand;

    private void Awake()
    {
        MoveSpeedStat.SetBaseValue(config.walkMoveSpeed);

        StaminaRestoreRateStat.SetBaseValue(config.staminaRestoreRate);

        MaxHealthStat.SetBaseValue(CalculateHPFromVigor(Vigor));
        HealthResource = new Resource(MaxHealthStat.Value);
        MaxHealthStat.OnStatChanged += (newMax) => HealthResource.SetMax(newMax, false);

        MaxStaminaStat.SetBaseValue(CalculateStaminaFromEndurance(Endurance));
        StaminaResource = new Resource(MaxStaminaStat.Value);
        MaxStaminaStat.OnStatChanged += (newMax) => StaminaResource.SetMax(newMax, false);

        MaxManaStat.SetBaseValue(CalculateManaFromMind(Mind));
        ManaResource = new Resource(MaxManaStat.Value);
        MaxManaStat.OnStatChanged += (newMax) => ManaResource.SetMax(newMax, false);
    }

    private void OnEnable()
    {
        equipment.OnActiveWeaponCycled += RecalculateAttackPower;

        RecalculateAttackPower(EquipmentSlot.RightHand, equipment.RightHand);
        RecalculateAttackPower(EquipmentSlot.LeftHand, equipment.LeftHand);
    }

    private void OnDisable()
    {
        equipment.OnActiveWeaponCycled -= RecalculateAttackPower;
    }

    private void Update()
    {
        health = $"{HealthResource.Current} '/' {HealthResource.Max}";
        stamina = $"{StaminaResource.Current} '/' {StaminaResource.Max}";
        mana = $"{ManaResource.Current} '/' {ManaResource.Max}";
        leftHand = $"{LeftHandAttackStat.Value}";
        rightHand = $"{RightHandAttackStat.Value}"; 
    }

    public void UpgradeAttribute(StatType statType)
    {
        switch (statType)
        {
            case StatType.Vigor:
                Vigor++;
                MaxHealthStat.SetBaseValue(CalculateHPFromVigor(Vigor));
                break;
            case StatType.Endurance:
                Endurance++;
                MaxStaminaStat.SetBaseValue(CalculateStaminaFromEndurance(Endurance));
                break;
            case StatType.Mind:
                Mind++;
                MaxManaStat.SetBaseValue(CalculateManaFromMind(Mind));
                break;
            case StatType.Strength:
                Strength++;
                RecalculateAttackPower(EquipmentSlot.RightHand, equipment.RightHand);
                RecalculateAttackPower(EquipmentSlot.LeftHand, equipment.LeftHand);
                break;
            case StatType.Dexterity:
                Dexterity++;
                RecalculateAttackPower(EquipmentSlot.RightHand, equipment.RightHand);
                RecalculateAttackPower(EquipmentSlot.LeftHand, equipment.LeftHand);
                break;
        }
    }

    private float CalculateHPFromVigor(int vigor) => vigor * 10f;
    private float CalculateStaminaFromEndurance(int endurance) => endurance * 5f;
    private float CalculateManaFromMind(int mind) => mind * 5f;
    private void RecalculateAttackPower(EquipmentSlot slot, WeaponInstance weapon)
    {
        float totalDamage = 0f;

        if (weapon.WeaponData == null)
        {
            totalDamage = (Strength + Dexterity) * 1.5f;
        }
        else
        {
            float strBonus = Strength * weapon.WeaponData.StrengthScaling;
            float dexBonus = Dexterity * weapon.WeaponData.DexterityScaling;
            totalDamage = weapon.WeaponData.BaseDamage + strBonus + dexBonus;
        }

        if (slot == EquipmentSlot.RightHand)
        {
            RightHandAttackStat.SetBaseValue(totalDamage);
        }
        else
        {
            LeftHandAttackStat.SetBaseValue(totalDamage);
        }
    }

    private float GetLevelUpCost()
    {
        return Mathf.Round(baseLevelUpCost * Mathf.Pow(Level, levelCostMultiplier));
    }
}

public enum StatType
{
    Vigor,
    Endurance,
    Strength,
    Dexterity,
    Mind
}

