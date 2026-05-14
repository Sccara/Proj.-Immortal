using UnityEngine;

public class EnemyUIManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] EnemyHealthController health;
    [SerializeField] EnemyPoiseController poise;

    [Header("UI")]
    [SerializeField] private ResourceBarUI healthBar;
    [SerializeField] private ResourceBarUI poiseBar;

    private void Start()
    {
        healthBar.Init(health.Health);
        poiseBar.Init(poise.Poise);
    }
}
