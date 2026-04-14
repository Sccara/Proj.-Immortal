using UnityEngine;
using UnityEngine.UI;

public class StaminaSystemUI : MonoBehaviour
{
    [SerializeField] private Image staminaBar;

    private void Awake()
    {
        PlayerManager.Instance.Attributes.StaminaResource.OnValueChanged += UpdateStaminaBar;
    }

    private void Start()
    {
        UpdateStaminaBar(PlayerManager.Instance.Attributes.StaminaResource.Percent);
    }

    public void UpdateStaminaBar(float staminaPercent)
    {
        staminaBar.fillAmount = staminaPercent;
    }

    private void OnDestroy()
    {
        if (PlayerManager.Instance.Attributes != null)
            PlayerManager.Instance.Attributes.StaminaResource.OnValueChanged -= UpdateStaminaBar;
    }
}
