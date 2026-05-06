using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    [SerializeField] private EnemyHealth _healthSystem;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _healthSystem = GetComponentInParent<EnemyHealth>();
        _healthSystem.Health.OnValueChanged += UpdateHealthBar;
    }

    public void UpdateHealthBar(float healthPercent)
    {
        healthBar.fillAmount = healthPercent;
    }

    private void OnDestroy()
    {
        if (_healthSystem != null)
            _healthSystem.Health.OnValueChanged -= UpdateHealthBar;
    }
}
