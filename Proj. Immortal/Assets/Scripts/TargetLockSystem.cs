using Unity.Cinemachine;
using UnityEngine;

public class TargetLockSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float lockOnRadius = 15f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private CinemachineCamera playerCamera; // Ссылка на главную камеру
    [SerializeField] private InputReader inputReader;

    public Transform CurrentTarget { get; private set; }
    public bool IsLockedOn => CurrentTarget != null;

    private void Start()
    {
        inputReader.OnLockOnPressed += ToggleLockOn;
    }

    private void Update()
    {
        // Если цель умерла или ушла слишком далеко — сбрасываем таргет
        if (IsLockedOn)
        {
            if (!CurrentTarget.gameObject.activeInHierarchy ||
                Vector3.Distance(transform.position, CurrentTarget.position) > lockOnRadius * 1.5f)
            {
                ClearTarget();
            }
        }
    }

    // Вызывается из InputReader (по нажатию кнопки Lock-on, например, R3 или СКМ)
    public void ToggleLockOn()
    {
        if (IsLockedOn)
        {
            ClearTarget();
        }
        else
        {
            FindNewTarget();
        }
    }

    private void FindNewTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, lockOnRadius, enemyLayer);

        if (enemies.Length == 0)
            return;

        float closestDistance = Mathf.Infinity;
        Transform bestTarget = null;

        foreach (var enemy in enemies)
        {
            // Здесь можно добавить проверку видимости (Raycast), чтобы не лочить врагов за стенами
            Vector3 directionToEnemy = enemy.transform.position - transform.position;
            float distance = directionToEnemy.sqrMagnitude;

            // Можно также проверять угол от центра экрана (playerCamera.forward), 
            // чтобы лочить того, кто ближе к прицелу, а не просто ближе физически.
            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestTarget = enemy.transform;
            }
        }

        CurrentTarget = bestTarget;
        SetLockOnCamera(1);
    }

    private void ClearTarget()
    {
        CurrentTarget = null;
        SetLockOnCamera(-1);
    }

    private void SetLockOnCamera(int priority)
    {
        playerCamera.Priority = priority;
        playerCamera.Target.LookAtTarget = CurrentTarget;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lockOnRadius);
    }
}
