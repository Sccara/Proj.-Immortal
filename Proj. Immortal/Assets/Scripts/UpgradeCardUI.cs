using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCardUI : MonoBehaviour
{
    [SerializeField] private Image upgradeIcon;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Button confirmButton;

    public UpgradeSO _upgrade;

    public void Init(UpgradeSO upgrade, Action<UpgradeSO> onSelected)
    {
        upgradeIcon.sprite = upgrade.icon;
        descriptionText.text = upgrade.description;
        _upgrade = upgrade;
        confirmButton.onClick.AddListener(() => { onSelected?.Invoke(upgrade); });
    }
}
