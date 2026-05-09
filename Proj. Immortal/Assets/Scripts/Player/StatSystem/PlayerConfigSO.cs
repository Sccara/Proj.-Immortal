using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Player")]
public class PlayerConfigSO : ScriptableObject
{
    [Header("Stats")]
    public int Vigor = 10;
    public int Endurance = 10;
    public int Strength = 10;
    public int Dexterity = 10;
    public float staminaRestoreRate;
    [Header("Movement")]
    public float walkMoveSpeed;
    public float sprintMoveSpeed;
    public float useItemSpeed;
    public float rotateSpeed;
    public float sprintStamina;
    public float jumpForce;
    public float jumpStamina;
    public float fallMultiplier;
    [Header("Combat")]
    public float staggerTime;
    public float attackRange;
    public float knockbackStrength;
    public float attackStepForce;
    public float maxChargeTime;
    public float attackStamina;
    public float heavyAttackStamina;
    public float minDamageMultiplier;
    public float maxDamageMultiplier;
    public float maxPoiseMultiplier;
    public float lightAttackCooldown;
    public float heavyAttackCooldown;
    [Header("Roll")]
    public float rollDuration;
    public float rollBaseSpeed;
    public AnimationCurve rollSpeedCurve;
    public float rollStamina;
}
