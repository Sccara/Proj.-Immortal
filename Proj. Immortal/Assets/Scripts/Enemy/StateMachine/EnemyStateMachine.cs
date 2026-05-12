using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(EnemySensor), typeof(Animator))]
public class EnemyStateMachine : MonoBehaviour
{
    private EnemyBaseState _currentState;
    private EnemyStateFactory _states;

    public EnemyConfigSO Config;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private EnemySensor sensor;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyHealthController health;

    public EnemyHealthController Health => health;
    public EnemySensor Sensor => sensor;
    public Animator Animator => animator;
    public NavMeshAgent Agent => agent;
    public EnemyBaseState CurrentState { get => _currentState; set { _currentState = value; } }
    public EnemyStateFactory States => _states;

    private void Awake()
    {
        _states = _states = new EnemyStateFactory(this);
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
}
