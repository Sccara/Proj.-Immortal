using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

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
    }

    [SerializeField] private float _staminaRestoreRate;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float poiseDamage;
    [SerializeField] private float poise;
    [SerializeField] private float maxPoise;

    public float StaminaRestoreRate { get => _staminaRestoreRate; set { _staminaRestoreRate = value; } }
    public float MoveSpeed { get => moveSpeed; set { moveSpeed = value; } }
    public float AttackDamage { get => attackDamage; set { attackDamage = value; } }
    public float AttackCooldown { get => attackCooldown; set { attackCooldown = value; } }
    public float PoiseDamage { get => poiseDamage; set { poiseDamage = value; } }
    public float Poise { get => poise; set { poise = value; } }
    public float MaxPoise { get => maxPoise; set { maxPoise = value; } }
}
