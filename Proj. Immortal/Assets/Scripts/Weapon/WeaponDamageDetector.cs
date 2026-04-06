using System.Collections.Generic;
using UnityEngine;

public class WeaponDamageDetector : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private List<Transform> hitPoints; // Точки вдоль лезвия (от гарды до кончика)
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private bool _debugGizmos = true;

    private bool _isAttacking;
    private float _currentDamage;
    private float _currentPoiseDamage;
    private Vector3 _currentKnockback;

    // Храним позиции точек в прошлом кадре
    private Dictionary<Transform, Vector3> _lastPointPositions = new Dictionary<Transform, Vector3>();

    // Список врагов, которых мы УЖЕ ударили за этот замах (чтобы не бить каждый кадр)
    private HashSet<IDamageable> _hitTargets = new HashSet<IDamageable>();

    private void Start()
    {
        // Инициализируем словарь позиций
        foreach (var point in hitPoints)
        {
            _lastPointPositions[point] = point.position;
        }
    }

    // Включаем проверку урона и передаем параметры текущей атаки
    public void EnableDamage(float damage, float poiseDamage, Vector3 knockback)
    {
        _currentDamage = damage;
        _currentPoiseDamage = poiseDamage;
        _currentKnockback = knockback;

        _hitTargets.Clear();
        // Сбрасываем позиции на текущие, чтобы первый луч не улетел в бесконечность
        ResetPositions();
        _isAttacking = true;
    }

    public void DisableDamage()
    {
        _isAttacking = false;
    }

    private void ResetPositions()
    {
        foreach (var point in hitPoints)
        {
            _lastPointPositions[point] = point.position;
        }
    }

    private void Update()
    {
        if (!_isAttacking)
            return;

        foreach (var point in hitPoints)
        {
            Vector3 lastPos = _lastPointPositions[point];
            Vector3 currentPos = point.position;

            // Самая важная часть: вектор от старой позиции к новой
            Vector3 direction = currentPos - lastPos;
            float distance = direction.magnitude;

            if (distance > 0.001f) // Если точка сдвинулась
            {
                // Пускаем луч сквозь "пространство" между кадрами
                RaycastHit hit;
                if (Physics.Raycast(lastPos, direction.normalized, out hit, distance, enemyLayer))
                {
                    if (hit.collider.TryGetComponent(out IDamageable damageable))
                    {
                        // Если мы еще не били этого врага в этом замахе
                        if (!_hitTargets.Contains(damageable))
                        {
                            DamageInfo info = new DamageInfo
                            {
                                DamageAmount = _currentDamage,
                                KnockbackForce = _currentKnockback,
                                PoiseDecreaseAmount = _currentPoiseDamage
                            };

                            damageable.TakeDamage(info);
                            _hitTargets.Add(damageable); // Запоминаем врага

                            // Тут можно добавить микро-паузу (HitStop) или партиклы
                            // Debug.Log($"Hit {hit.collider.name}");
                        }
                    }
                }
            }

            // Обновляем старую позицию
            _lastPointPositions[point] = currentPos;
        }
    }

    private void OnDrawGizmos()
    {
        if (!_debugGizmos || hitPoints == null) return;

        Gizmos.color = _isAttacking ? Color.red : Color.green;
        foreach (var point in hitPoints)
        {
            if (_lastPointPositions.ContainsKey(point))
            {
                // Рисуем лучи, которые сработали в Update
                Gizmos.DrawLine(_lastPointPositions[point], point.position);
            }
            // Рисуем сами точки
            Gizmos.DrawWireSphere(point.position, 0.02f);
        }
    }
}
