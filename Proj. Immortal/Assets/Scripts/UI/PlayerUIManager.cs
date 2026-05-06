using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerAttributes attributes; 

    [Header("View")]
    [SerializeField] private ResourceBarUI healthBar;
    [SerializeField] private ResourceBarUI manaBar;
    [SerializeField] private ResourceBarUI staminaBar;

    private void Start()
    {
        healthBar.Init(attributes.HealthResource);
        manaBar.Init(attributes.ManaResource);
        staminaBar.Init(attributes.StaminaResource);
    }
}
