using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    private EnemyHealth _healthSystem;

    private void Awake()
    {
        Init();
        _healthSystem.Health.OnValueChanged += UpdateHealthBar;
    }

    public void Init()
    {
        _healthSystem = GetComponentInParent<EnemyHealth>();
    }

    public void UpdateHealthBar(float healthPercent)
    {
        healthBar.fillAmount = healthPercent;
    }

    private void OnDisable()
    {
        if (_healthSystem != null)
            _healthSystem.Health.OnValueChanged -= UpdateHealthBar;
    }
}
