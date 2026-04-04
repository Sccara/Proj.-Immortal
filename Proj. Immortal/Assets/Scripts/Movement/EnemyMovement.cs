using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform _player;
    private NavMeshAgent _agent;

    public NavMeshAgent Agent => _agent;

    private void Start()
    {
        _player = GameObject.Find("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_agent.enabled == true)
        {
            _agent.SetDestination(_player.transform.position);
        }
    }
}
