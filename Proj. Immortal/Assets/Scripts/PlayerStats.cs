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

    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackCooldown;

    public float MoveSpeed { get => moveSpeed; set { moveSpeed = value; } }
    public float AttackDamage { get => attackDamage; set { attackDamage = value; } }
    public float AttackCooldown { get => attackCooldown; set { attackCooldown = value; } }
}
