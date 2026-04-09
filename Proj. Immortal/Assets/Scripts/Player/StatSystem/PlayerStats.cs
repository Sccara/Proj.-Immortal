using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [SerializeField] private PlayerStatsConfigSO config;
    [SerializeField] private float baseLevelUpCost = 100f;
    [SerializeField] private float levelCostMultiplier = 1.2f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Initialize(config);
    }

    [Header("Leveling")]
    [field: SerializeField] public int Level { get; set; } = 1;
    [field: SerializeField] public float CurrentSouls { get; set; }
    public float LevelUpCost { get => GetLevelUpCost(); private set { } }
    [Header("Stats")]
    [field: SerializeField] public Resource Health { get; private set; }
    [field: SerializeField] public Resource Stamina { get; private set; }
    [field: SerializeField] public Resource Poise { get; private set; }
    [field: SerializeField] public float StaminaRestoreRate { get; set; }
    [field: SerializeField] public float AttackDamage { get; set; }
    [field: SerializeField] public float PoiseDamage { get; set; }
    [field: SerializeField] public float PoiseRestoreCooldown { get; set; }
    [field: SerializeField]  public float PoiseRestoreMultiplier { get; set; }
    [Header("Movement")]
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float SprintMoveSpeed { get; set; }
    [field: SerializeField] public float WalkMoveSpeed { get; set; }
    [field: SerializeField] public float RotateSpeed { get; set; }
    [field: SerializeField] public float SprintStamina { get; set; }
    [field: SerializeField] public float JumpForce { get; set; }
    [field: SerializeField] public float JumpStamina { get; set; }
    [field: SerializeField]public float FallMultiplier { get; set; }
    [Header("Combat")]
    [field: SerializeField] public float StaggerTime { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }
    [field: SerializeField] public float KnockbackStrength { get; set; }
    [field: SerializeField] public float AttackStepForce { get; set; }
    [field: SerializeField] public float MaxChargeTime { get; set; }
    [field: SerializeField] public float AttackStamina { get; set; }
    [field: SerializeField] public float HeavyAttackStamina { get; set; }
    [field: SerializeField] public float MinDamageMultiplier { get; set; }
    [field: SerializeField] public float MaxDamageMultiplier { get; set; }
    [field: SerializeField] public float MaxPoiseMultiplier { get; set; }
    [field: SerializeField] public float LightAttackCooldown { get; set; }
    [field: SerializeField] public float HeavyAttackCooldown { get; set; }
    [Header("Dash")]
    [field: SerializeField] public float DashForce { get; set; }
    [field: SerializeField] public float DashCooldown { get; set; }
    [field: SerializeField] public float DashDuration { get; set; }
    [field: SerializeField] public float DashStamina { get; set; }

    private void Initialize(PlayerStatsConfigSO cfg)
    {
        if (cfg == null)
        {
            Debug.LogError("PlayerStats: Config SO is missing!");
            return;
        }

        Health = new Resource(cfg.maxHealth);

        Stamina = new Resource(cfg.maxStamina);
        StaminaRestoreRate = cfg.staminaRestoreRate;

        Poise = new Resource(cfg.maxPoise);

        AttackDamage = cfg.attackDamage;
        PoiseDamage = cfg.poiseDamage;
        PoiseRestoreCooldown = cfg.poiseRestoreCooldown;
        PoiseRestoreMultiplier = cfg.poiseRestoreMultiplier;

        WalkMoveSpeed = cfg.walkMoveSpeed;
        SprintMoveSpeed = cfg.sprintMoveSpeed;
        MoveSpeed = WalkMoveSpeed;
        RotateSpeed = cfg.rotateSpeed;
        SprintStamina = cfg.sprintStamina;
        JumpForce = cfg.jumpForce;
        JumpStamina = cfg.jumpStamina;
        FallMultiplier = cfg.fallMultiplier;

        AttackRange = cfg.attackRange;
        KnockbackStrength = cfg.knockbackStrength;
        AttackStepForce = cfg.attackStepForce;
        MaxChargeTime = cfg.maxChargeTime;
        StaggerTime = cfg.staggerTime;

        AttackStamina = cfg.attackStamina;
        HeavyAttackStamina = cfg.heavyAttackStamina;

        MinDamageMultiplier = cfg.minDamageMultiplier;
        MaxDamageMultiplier = cfg.maxDamageMultiplier;
        MaxPoiseMultiplier = cfg.maxPoiseMultiplier;

        LightAttackCooldown = cfg.lightAttackCooldown;
        HeavyAttackCooldown = cfg.heavyAttackCooldown;

        DashForce = cfg.dashForce;
        DashCooldown = cfg.dashCooldown;
        DashDuration = cfg.dashDuration;
        DashStamina = cfg.dashStamina;

        Debug.Log("<color=green>PlayerStats initialized successfully from SO.</color>");
    }

    private float GetLevelUpCost()
    {
        return Mathf.Round(baseLevelUpCost * Mathf.Pow(Level, levelCostMultiplier));
    }
}
