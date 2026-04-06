using UnityEngine;
using UnityEngine.UI;

public class HealthSystemUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    private void Awake()
    {
        HealthSystemController.OnHealthChanged += UpdateHealthBar;
    }

    public void UpdateHealthBar(float healthPercent)
    {
        healthBar.fillAmount = healthPercent;
    }

    private void OnDisable()
    {
        HealthSystemController.OnHealthChanged -= UpdateHealthBar;
    }
}
