using UnityEngine;
using UnityEngine.UI;

public class HealthSystemUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    private void Awake()
    {
        PlayerManager.Instance.Attributes.HealthResource.OnValueChanged += UpdateHealthBar;
    }
    private void Start()
    {
        UpdateHealthBar(PlayerManager.Instance.Attributes.HealthResource.Percent);
    }

    public void UpdateHealthBar(float healthPercent)
    {
        healthBar.fillAmount = healthPercent;
    }

    private void OnDisable()
    {
        if (PlayerManager.Instance.Attributes != null)
            PlayerManager.Instance.Attributes.HealthResource.OnValueChanged += UpdateHealthBar;
    }
}
