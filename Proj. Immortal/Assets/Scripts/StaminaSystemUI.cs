using UnityEngine;
using UnityEngine.UI;

public class StaminaSystemUI : MonoBehaviour
{
    [SerializeField] private Image staminaBar;

    public void UpdateStaminaBar(float staminaPercent)
    {
        staminaBar.fillAmount = staminaPercent;
    }
}
