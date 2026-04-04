using UnityEngine;
using UnityEngine.UI;

public class HealthSystemUI : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    public void UpdateHealthBar(float healthPercent)
    {
        healthBar.fillAmount = healthPercent;
    }
}
