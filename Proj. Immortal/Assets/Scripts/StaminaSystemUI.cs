using UnityEngine;
using UnityEngine.UI;

public class StaminaSystemUI : MonoBehaviour
{
    [SerializeField] private Image staminaBar;

    private void Awake()
    {
        PlayerStats.Instance.Stamina.OnValueChanged += UpdateStaminaBar;
    }

    private void Start()
    {
        UpdateStaminaBar(PlayerStats.Instance.Stamina.Percent);
    }

    public void UpdateStaminaBar(float staminaPercent)
    {
        staminaBar.fillAmount = staminaPercent;
    }

    private void OnDestroy()
    {
        if (PlayerStats.Instance != null)
            PlayerStats.Instance.Stamina.OnValueChanged -= UpdateStaminaBar;
    }
}
