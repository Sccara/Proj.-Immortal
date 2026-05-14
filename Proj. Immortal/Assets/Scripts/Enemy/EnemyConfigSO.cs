using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Config", menuName = "Enemy")]
public class EnemyConfigSO : ScriptableObject
{
    [Header("Stats")]
    public float Health;
    public float Poise;

    [Header("Movement")]
    public float WalkMoveSpeed;
    public float ChaseSpeed;
    public float RotateSpeed;

    [Header("Sensors Settings")]
    public float ViewRange;
    [Range(0, 360)]
    public float ViewAngle;
    public float DetectRadius;
    public float LoseSightRange;

    [Header("Combat Settings")]
    public float AttackRange;
    public float StrafeWaitTime;
    public float KnockbackDuration;
}
