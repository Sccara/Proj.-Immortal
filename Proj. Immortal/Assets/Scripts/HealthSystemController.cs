using UnityEngine;

public class HealthSystemController : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private HealthSystemUI healthSystemUI;
    public HealthSystem _healthSystem;

    private void Start()
    {
        _healthSystem = new HealthSystem(100, 100);
        healthSystemUI.UpdateHealthBar(_healthSystem.HealthPercent);
    }

    public void TakeDamage(DamageInfo info)
    {
        _healthSystem.TakeDamage(info.Amount);
        healthSystemUI.UpdateHealthBar(_healthSystem.HealthPercent);

        if (_healthSystem.Health <= 0)
        {
            loseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Heal(float healAmount)
    {
        _healthSystem.Heal(healAmount);
        healthSystemUI.UpdateHealthBar(_healthSystem.HealthPercent);
    }
}
