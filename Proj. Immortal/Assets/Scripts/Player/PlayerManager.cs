using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private HealthSystemController health;
    [SerializeField] private StaminaSystemController stamina;
    [SerializeField] private LevelSystemController level;
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerSpellMemory spellMemory;
    [SerializeField] private QuickItemsSystem quickItems;
    [SerializeField] private PlayerEquipment equipment;
    [SerializeField] private PlayerAttributes playerAttributes;
    [SerializeField] private PlayerConfigSO config;

    public HealthSystemController Health { get => health; private set { } }
    public StaminaSystemController Stamina { get => stamina; private set { } }
    public LevelSystemController Level { get => level; private set { } }
    public PlayerCombat Combat { get => combat; private set { } }
    public QuickItemsSystem QuickItems { get => quickItems; private set { } }
    public PlayerEquipment Equipment { get => equipment; private set { } }
    public Inventory Inventory { get => inventory; private set { } }    
    public PlayerSpellMemory SpellMemory { get => spellMemory; private set { } }
    public PlayerAttributes Attributes { get => playerAttributes; private set { } }
    public PlayerConfigSO Config { get => config; private set { } }

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
        
    }
}
