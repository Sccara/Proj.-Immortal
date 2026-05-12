using System;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{
    [SerializeField] private EnemyConfigSO config;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;

    [SerializeField] private Vector3 eyeOffset = new Vector3(0, 1.5f, 0);
    
    [SerializeField] private Transform _playerTransform;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _playerTransform = playerObj.transform;
        }
    }

    public bool IsPlayerInAgroRadius()
    {
        Collider[] playerCollider = Physics.OverlapSphere(transform.position, config.DetectRadius, playerLayer);

        return playerCollider.Length > 0;
    }

    public bool IsLostPlayerSight()
    {
        if (_playerTransform == null)
        {
            return true;
        }

        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        return distance >= config.LoseSightRange;
    }

    public bool IsPlayerInViewSight()
    {
        if (_playerTransform == null)
        {
            return false;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
        if (distanceToPlayer > config.ViewRange)
        {
            return false;
        }

        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (angleToPlayer > config.ViewAngle / 2f)
        {
            return false;
        }

        Vector3 eyePosition = transform.position + eyeOffset;
        Vector3 playerCenter = _playerTransform.position + eyeOffset;

        if (Physics.Linecast(eyePosition, playerCenter, obstacleLayer))
        {
            return false;
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        if (config == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, config.DetectRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, config.LoseSightRange);

        Gizmos.color = Color.blue;
        Vector3 eyePos = transform.position + eyeOffset;

        Vector3 rightVisionLimit = Quaternion.Euler(0, config.ViewAngle / 2, 0) * transform.forward;
        Vector3 leftVisionLimit = Quaternion.Euler(0, -config.ViewAngle / 2, 0) * transform.forward;

        Gizmos.DrawRay(eyePos, rightVisionLimit * config.ViewRange);
        Gizmos.DrawRay(eyePos, leftVisionLimit * config.ViewRange);

        if (_playerTransform != null)
        {
            Gizmos.color = IsPlayerInViewSight() ? Color.green : Color.gray;
            Gizmos.DrawLine(eyePos, _playerTransform.position + eyeOffset);
        }
    }
}
