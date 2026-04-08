using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private HealthSystemController health;
    [SerializeField] private StaminaSystemController stamina;
    [SerializeField] private LevelSystemController level;
    [SerializeField] private PlayerCombat combat;

    private PlayerStats _stats;

    public HealthSystemController Health { get => health; private set { } }
    public StaminaSystemController Stamina { get => stamina; private set { } }
    public LevelSystemController Level { get => level; private set { } }
    public PlayerCombat Combat { get => combat; private set { } }
    public PlayerStats Stats { get => _stats; private set { } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _stats = PlayerStats.Instance;
    }
}
