using UnityEngine;
using UnityEngine.UI;

public class LevelSystemController : MonoBehaviour
{
    [SerializeField] private LevelSystemUI levelSystemUI;
    [SerializeField] private GameObject upgradePanel;
    public LevelSystem _levelSystem;

    private void Awake()
    {
        _levelSystem = new LevelSystem(1.2f, 100);
        _levelSystem.OnLevelUp += ShowUpgradePanel;
    }

    private void Start()
    {
        levelSystemUI.UpdateLevelBar(_levelSystem.ExpPercent, _levelSystem.Level);
    }

    public void AddExp(float expAmount)
    {
        _levelSystem.AddExp(expAmount);
        levelSystemUI.UpdateLevelBar(_levelSystem.ExpPercent, _levelSystem.Level);
    }

    public void ShowUpgradePanel()
    {
        upgradePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}
