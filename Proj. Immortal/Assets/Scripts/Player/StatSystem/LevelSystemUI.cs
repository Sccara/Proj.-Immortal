using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystemUI : MonoBehaviour
{
    [SerializeField] private Image levelBar;
    [SerializeField] private TextMeshProUGUI levelText;

    public void UpdateLevelBar(float expPercent, int level)
    {
        levelBar.fillAmount = expPercent;
        levelText.text = level.ToString();
    }
}
