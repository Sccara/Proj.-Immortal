using System.Collections.Generic;
using UnityEngine;

public class WeaponDamageDetector : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<Transform> hitPoints; 
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float hitRadius = 0.1f;
    [SerializeField] private bool _debugGizmos = true;

    private bool _isAttacking;
    private float _currentDamage;
    private float _currentPoiseDamage;
    private Vector3 _currentKnockback;


    private Vector3[] _lastPointPositions;
    //private Dictionary<Transform, Vector3> _lastPointPositions = new Dictionary<Transform, Vector3>();

    private HashSet<IDamageable> _hitTargets = new HashSet<IDamageable>();

    private void Start()
    {
        _lastPointPositions = new Vector3[hitPoints.Count];
        ResetPositions();
    }

    public void EnableDamage(float damage, float poiseDamage, Vector3 knockback)
    {
        _currentDamage = damage;
        _currentPoiseDamage = poiseDamage;
        _currentKnockback = knockback;

        _hitTargets.Clear();

        ResetPositions();
        _isAttacking = true;
    }

    public void DisableDamage()
    {
        _isAttacking = false;
    }

    private void ResetPositions()
    {
        for (int i = 0; i < hitPoints.Count; i++)
        {
            _lastPointPositions[i] = hitPoints[i].position;
        }
    }

    private void LateUpdate()
    {
        if (!_isAttacking)
            return;

        for (int i = 0; i < hitPoints.Count; i++)
        {
            Vector3 lastPos = _lastPointPositions[i];
            Vector3 currentPos = hitPoints[i].position;


            Vector3 direction = currentPos - lastPos;
            float distance = direction.magnitude;

            if (distance > 0.001f)
            {
                if (Physics.SphereCast(lastPos, hitRadius, direction.normalized, out RaycastHit hit, distance, targetLayer))
                {
                    IDamageable target = hit.collider.GetComponentInParent<IDamageable>();

                    if (target != null)
                    {
                        if (!_hitTargets.Contains(target))
                        {
                            DamageInfo info = new DamageInfo
                            {
                                DamageAmount = _currentDamage,
                                KnockbackForce = _currentKnockback,
                                PoiseDecreaseAmount = _currentPoiseDamage
                            };

                            target.TakeDamage(info);
                            _hitTargets.Add(target);
                        }
                    }
                }
            }

            _lastPointPositions[i] = currentPos;
        }
    }

    private void OnDrawGizmos()
    {
        if (!_debugGizmos || hitPoints == null) return;

        Gizmos.color = _isAttacking ? Color.red : Color.green;
        if (Application.isPlaying && _lastPointPositions != null)
        {
            for (int i = 0; i < hitPoints.Count; i++)
            {
                Gizmos.DrawLine(_lastPointPositions[i], hitPoints[i].position);
                Gizmos.DrawWireSphere(hitPoints[i].position, hitRadius); // Рисуем толщину лезвия
            }
        }
        else 
        {
            foreach (var point in hitPoints)
            {
                if (point != null) Gizmos.DrawWireSphere(point.position, hitRadius);
            }
        }
    }
}
