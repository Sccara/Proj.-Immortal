using UnityEngine;
using UnityEngine.UI;

public class StaminaSystemUI : MonoBehaviour
{
    [SerializeField] private Image staminaBar;
    [SerializeField] private PlayerAttributes attributes;

    private void Awake()
    {
        attributes.StaminaResource.OnValueChanged += UpdateStaminaBar;
    }

    private void Start()
    {
        UpdateStaminaBar(attributes.StaminaResource.Percent);
    }

    public void UpdateStaminaBar(float staminaPercent)
    {
        staminaBar.fillAmount = staminaPercent;
    }

    private void OnDestroy()
    {
        if (attributes != null)
            attributes.StaminaResource.OnValueChanged -= UpdateStaminaBar;
    }
}
