using UnityEngine;

public class EnemyPoiseController : MonoBehaviour
{
    [SerializeField] private EnemyConfigSO config;

    private float _lastDamageTime;

    public Resource Poise { get; set; }

    private void Awake()
    {
        Poise = new Resource(config.Poise);
    }

    private void Update()
    {
        RegenPoise();
    }

    private void RegenPoise()
    {
        if (Poise.Current > 0 && Poise.Current < Poise.Max && Time.time >= _lastDamageTime + config.TimeBeforePoiseRegen)
        {
            Poise.Restore(config.PoiseRegenRate * Time.deltaTime);
        }
    }

    public void TakePoiseDamage(float amount)
    {
        if (Poise.Current <= 0)
            return;

        Poise.Use(amount);
        _lastDamageTime = Time.time;

        Debug.Log($"Poise damage taken: {amount}. Current Poise: {Poise.Current}");
    }

    public void ResetPoise()
    {
        Poise.Restore(Poise.Max);
    }
}
