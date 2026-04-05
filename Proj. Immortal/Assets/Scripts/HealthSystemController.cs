using System.Collections;
using UnityEngine;

public class HealthSystemController : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private HealthSystemUI healthSystemUI;
    public HealthSystem _healthSystem;

    [SerializeField] private float staggerTime;
    [SerializeField] private bool isStaggered;

    [SerializeField] private float timeSinceLastHit;
    [SerializeField] private float poiseRestoreCooldown;
    [SerializeField] private float poiseRestoreMultiplier;

    private void Start()
    {
        _healthSystem = new HealthSystem(150, 150);
        healthSystemUI.UpdateHealthBar(_healthSystem.HealthPercent);
    }

    private void Update()
    {
        if (timeSinceLastHit >= poiseRestoreCooldown && PlayerStats.Instance.Poise <= PlayerStats.Instance.MaxPoise)
        {
            PlayerStats.Instance.Poise += poiseRestoreMultiplier * Time.deltaTime;
            PlayerStats.Instance.Poise = Mathf.Clamp(PlayerStats.Instance.Poise, 0, PlayerStats.Instance.MaxPoise);
        }

        timeSinceLastHit += Time.deltaTime;
    }

    public void TakeDamage(DamageInfo info)
    {
        _healthSystem.TakeDamage(info.DamageAmount);
        healthSystemUI.UpdateHealthBar(_healthSystem.HealthPercent);
        PlayerStats.Instance.Poise -= info.PoiseDecreaseAmount;
        if (PlayerStats.Instance.Poise <= 0 && !isStaggered)
        {
            StartCoroutine(Stagger());
        }

        if (_healthSystem.Health <= 0)
        {
            loseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private IEnumerator Stagger()
    {
        isStaggered = true;
        GetComponent<PlayerCombat>().CancelAttack();
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerCombat>().enabled = false;
        Color oldColor = GetComponent<MeshRenderer>().material.color;
        GetComponent<MeshRenderer>().material.color = Color.black;

        yield return new WaitForSeconds(staggerTime);

        GetComponent<PlayerController>().enabled = true;
        GetComponent<PlayerCombat>().enabled = true;
        GetComponent<MeshRenderer>().material.color = oldColor;
        PlayerStats.Instance.Poise = PlayerStats.Instance.MaxPoise;
        isStaggered = false;
    }

    public void Heal(float healAmount)
    {
        _healthSystem.Heal(healAmount);
        healthSystemUI.UpdateHealthBar(_healthSystem.HealthPercent);
    }
}
