using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [SerializeField] private PlayerStatsConfigSO config;

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
    [Header("Stats")]
    [field: SerializeField] public float MaxHealth { get; set; }
    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public float MaxStamina { get; set; }
    [field: SerializeField] public float Stamina { get; set; }
    [field: SerializeField] public float StaminaRestoreRate { get; set; }
    [field: SerializeField] public float AttackDamage { get; set; }
    [field: SerializeField] public float PoiseDamage { get; set; }
    [field: SerializeField] public float Poise { get; set; }
    [field: SerializeField] public float MaxPoise { get; set; }
    [field: SerializeField] public float PoiseRestoreCooldown { get; set; }
    [field: SerializeField]  public float PoiseRestoreMultiplier { get; set; }
    [Header("Movement")]
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float RotateSpeed { get; set; }
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

        MaxHealth = cfg.maxHealth;
        Health = MaxHealth;

        MaxStamina = cfg.maxStamina;
        Stamina = MaxStamina;
        StaminaRestoreRate = cfg.staminaRestoreRate;

        AttackDamage = cfg.attackDamage;
        PoiseDamage = cfg.poiseDamage;
        PoiseRestoreCooldown = cfg.poiseRestoreCooldown;
        PoiseRestoreMultiplier = cfg.poiseRestoreMultiplier;

        MaxPoise = cfg.maxPoise;
        Poise = MaxPoise;

        MoveSpeed = cfg.moveSpeed;
        RotateSpeed = cfg.rotateSpeed;

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
}
