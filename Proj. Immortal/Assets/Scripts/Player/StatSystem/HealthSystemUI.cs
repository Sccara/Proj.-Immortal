using UnityEngine;
using UnityEngine.UI;

public class HealthSystemUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private PlayerAttributes attributes;

    private void OnEnable()
    {
        attributes.HealthResource.OnValueChanged += UpdateHealthBar;
    }
    private void Start()
    {
        UpdateHealthBar(attributes.HealthResource.Percent);
    }

    public void UpdateHealthBar(float healthPercent)
    {
        healthBar.fillAmount = healthPercent;
    }

    private void OnDisable()
    {
        if (attributes != null)
            attributes.HealthResource.OnValueChanged -= UpdateHealthBar;
    }
}
