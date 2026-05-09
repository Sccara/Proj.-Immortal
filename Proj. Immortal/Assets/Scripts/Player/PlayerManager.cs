using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private HealthSystemController health;
    [SerializeField] private StaminaSystemController stamina;
    [SerializeField] private ManaSystemController mana;
    [SerializeField] private LevelSystemController level;
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlayerSpellMemory spellMemory;
    [SerializeField] private PlayerQuickItems quickItems;
    [SerializeField] private PlayerEquipment equipment;
    [SerializeField] private PlayerAttributes playerAttributes;
    [SerializeField] private PlayerConfigSO config;

    public HealthSystemController Health => health;
    public StaminaSystemController Stamina => stamina;
    public ManaSystemController Mana => mana;
    public LevelSystemController Level => level;
    public PlayerCombat Combat => combat;
    public PlayerQuickItems QuickItems => quickItems;
    public PlayerEquipment Equipment => equipment;
    public Inventory Inventory => inventory;    
    public PlayerSpellMemory SpellMemory => spellMemory;
    public PlayerAttributes Attributes => playerAttributes;
    public PlayerConfigSO Config => config;
}
