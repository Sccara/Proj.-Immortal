using Unity.Cinemachine;
using UnityEngine;

public class TargetLockSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float lockOnRadius = 15f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private CinemachineCamera playerCamera;
    [SerializeField] private InputReader inputReader;

    public Transform CurrentTarget { get; private set; }
    public bool IsLockedOn { get; private set; }

    private Collider[] _enemyColliders = new Collider[10];

    private void OnEnable()
    {
        if (inputReader != null) 
            inputReader.OnLockOnPressed += ToggleLockOn;
    }

    private void OnDisable()
    {
        if (inputReader != null)
            inputReader.OnLockOnPressed -= ToggleLockOn;
    }

    private void Update()
    {
        if (IsLockedOn)
        {
            float breakRadius = lockOnRadius * 1.5f;
            if (CurrentTarget == null ||
                (CurrentTarget.position - transform.position).sqrMagnitude > breakRadius * breakRadius)
            {
                ClearTarget();
            }
        }
    }

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
        int count = Physics.OverlapSphereNonAlloc(transform.position, lockOnRadius, _enemyColliders, enemyLayer);

        if (_enemyColliders.Length == 0)
            return;

        float closestDistance = Mathf.Infinity;
        Transform bestTarget = null;

        for (int i = 0; i < count; i++)
        {
            Vector3 directionToEnemy = _enemyColliders[i].transform.position - transform.position;
            float distance = directionToEnemy.sqrMagnitude;

            if (distance < closestDistance)
            {
                closestDistance = distance;
                bestTarget = _enemyColliders[i].transform;
            }
        }

        IsLockedOn = true;
        CurrentTarget = bestTarget;
        SetLockOnCamera(1);
    }

    private void ClearTarget()
    {
        CurrentTarget = null;
        ClearLockOnCamera(-1);
        IsLockedOn = false;
    }

    private void SetLockOnCamera(int priority)
    {
        playerCamera.Priority = priority;
        playerCamera.Target.LookAtTarget = CurrentTarget;
    }

    private void ClearLockOnCamera(int priority)
    {
        playerCamera.Priority = priority;
        playerCamera.Target.LookAtTarget = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lockOnRadius);
    }
}
