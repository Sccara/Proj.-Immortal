using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "Player")]
public class PlayerStatsConfigSO : ScriptableObject
{
    [Header("Stats")]
    public float maxHealth;
    public float maxStamina;
    public float staminaRestoreRate;
    public float attackDamage;
    public float poiseDamage;
    public float maxPoise;
    public float poiseRestoreCooldown;
    public float poiseRestoreMultiplier;
   [Header("Movement")]
    public float moveSpeed;
    public float rotateSpeed;
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
    [Header("Dash")]
    public float dashForce;
    public float dashCooldown;
    public float dashDuration;
    public float dashStamina;
}
