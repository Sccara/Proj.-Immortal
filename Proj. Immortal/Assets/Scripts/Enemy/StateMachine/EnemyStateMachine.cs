using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemySensor))]
public class EnemyStateMachine : MonoBehaviour
{
    private EnemyBaseState _currentState;
    private EnemyStateFactory _states;

    public EnemyConfigSO Config;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemySensor sensor;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyHealthController health;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private EnemyCombat combat;

    public EnemyHealthController Health => health;
    public EnemySensor Sensor => sensor;
    public EnemyCombat Combat => combat;
    public Animator Animator => animator;
    public NavMeshAgent Agent => agent;
    public Rigidbody Rb => rb;
    public EnemyBaseState CurrentState { get => _currentState; set { _currentState = value; } }
    public EnemyStateFactory States => _states;
    public Vector3 DirectionToPlayer => (Sensor.PlayerTransform.position - transform.position).normalized;

    private void Awake()
    {
        _states = _states = new EnemyStateFactory(this);
        health.OnTakeHit += HandleTakeHit;
        health.OnDeath += HandleDeath;
    }

    private void Start()
    {
        CurrentState = States.Idle();
        CurrentState.EnterState();
    }

    private void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateStates();
        }
    }

    private void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.FixedUpdateStates();
        }
    }

    public void StopMovement()
    {
        Agent.isStopped = true;
        Agent.velocity = Vector3.zero;
        float currentSpeedRatio = Vector3.zero.magnitude / Config.ChaseSpeed;
        
        Animator.SetFloat("Speed", currentSpeedRatio);
    }

    private void HandleTakeHit(DamageInfo info)
    {
        CurrentState?.ExitState();
        CurrentState = States.Impact(info.KnockbackForce);
        CurrentState.EnterState();
    }

    private void HandleDeath()
    {
        CurrentState?.ExitState();
        CurrentState = States.Dead();
        CurrentState.EnterState();
    }

    private void OnDestroy()
    {
        health.OnTakeHit -= HandleTakeHit;
        health.OnDeath -= HandleDeath;
    }
}
