using UnityEngine;
using UnityEngine.UI;

public class HealthSystemUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    private void Awake()
    {
        PlayerStats.Instance.Health.OnValueChanged += UpdateHealthBar;
    }

    public void UpdateHealthBar(float healthPercent)
    {
        healthBar.fillAmount = healthPercent;
    }

    private void OnDisable()
    {
        if (PlayerStats.Instance != null)
            PlayerStats.Instance.Health.OnValueChanged += UpdateHealthBar;
    }
}
