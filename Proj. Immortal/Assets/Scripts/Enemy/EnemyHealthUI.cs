using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    [SerializeField] private EnemyHealthController _healthSystem;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _healthSystem = GetComponentInParent<EnemyHealthController>();
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
