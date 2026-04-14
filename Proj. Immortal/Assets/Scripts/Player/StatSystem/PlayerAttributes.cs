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
    [field: SerializeField] public int Vigor { get; private set; } = 10;
    [field: SerializeField] public int Endurance { get; private set; } = 10;
    [field: SerializeField] public int Strength { get; private set; } = 10;
    [field: SerializeField] public int Dexterity { get; private set; } = 10;

    [Header("Stats")]
    public Stat MaxHealthStat = new Stat();
    public Stat MaxStaminaStat = new Stat();
    public Stat RightHandAttackStat = new Stat();
    public Stat LeftHandAttackStat = new Stat();
    public Stat PoiseAttackPowerStat = new Stat();

    [Header("Hidden Stats")]
    public Stat MoveSpeedStat = new Stat();
    public Stat MaxPoiseStat = new Stat();
    public Stat StaminaRestoreRateStat = new Stat();

    [Header("Resources")]
    public Resource HealthResource;
    public Resource StaminaResource;
    public Resource PoiseResource;

    [Header("DEBUG")]
    public string health;
    public string stamina;
    public string poise;

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
    }

    private void Start()
    {
        PlayerManager.Instance.Equipment.OnWeaponEquipped += RecalculateAttackPower;

        RecalculateAttackPower(EquipmentSlot.RightHand, PlayerManager.Instance.Equipment.RightWeapon);
        RecalculateAttackPower(EquipmentSlot.LeftHand, PlayerManager.Instance.Equipment.LeftWeapon);
    }

    private void Update()
    {
        health = $"{HealthResource.Current} '/' {HealthResource.Max}";
        stamina = $"{StaminaResource.Current} '/' {StaminaResource.Max}";
        poise = $"{PoiseResource.Current} '/' {PoiseResource.Max}";
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
            case StatType.Strength:
                Strength++;
                RecalculateAttackPower(EquipmentSlot.RightHand, PlayerManager.Instance.Equipment.RightWeapon);
                RecalculateAttackPower(EquipmentSlot.LeftHand, PlayerManager.Instance.Equipment.LeftWeapon);
                break;
            case StatType.Dexterity:
                Dexterity++;
                RecalculateAttackPower(EquipmentSlot.RightHand, PlayerManager.Instance.Equipment.RightWeapon);
                RecalculateAttackPower(EquipmentSlot.LeftHand, PlayerManager.Instance.Equipment.LeftWeapon);
                break;
        }

        Debug.Log($"Прокачан {statType}. Новые значения пересчитаны!");
    }

    private float CalculateHPFromVigor(int vigor) => vigor * 10f;
    private float CalculateStaminaFromEndurance(int endurance) => endurance * 5f;
    private void RecalculateAttackPower(EquipmentSlot slot, WeaponSO weapon)
    {
        float totalDamage = 0f;

        if (weapon == null)
        {
            // Удары кулаками (можно завести отдельную константу для этого)
            totalDamage = (Strength + Dexterity) * 1.5f;
        }
        else
        {
            // Считаем урон от скейлов оружия
            float strBonus = Strength * weapon.StrengthScaling;
            float dexBonus = Dexterity * weapon.DexterityScaling;
            totalDamage = weapon.BaseDamage + strBonus + dexBonus;
        }

        // Применяем урон в нужный стат
        if (slot == EquipmentSlot.RightHand)
        {
            RightHandAttackStat.SetBaseValue(totalDamage);
            Debug.Log($"Урон Правой руки: {totalDamage}");
        }
        else
        {
            LeftHandAttackStat.SetBaseValue(totalDamage);
            Debug.Log($"Урон Левой руки: {totalDamage}");
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
    Dexterity
}

