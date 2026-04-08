using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
    public static Action<UpgradeSO> OnCardSelected;

    [SerializeField] private List<UpgradeSO> upgrades;

    [SerializeField] private GameObject menu;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject upgradeCardPrefab;

    private void Awake()
    {
        OnCardSelected += HandleUpgradeCard;
    }

    private void OnEnable()
    {
        List<UpgradeSO> temp = new List<UpgradeSO>();
        temp.AddRange(upgrades);

        foreach (var card in content.transform.GetComponentsInChildren<UpgradeCardUI>()) 
        {
            UpgradeSO upgrade = temp[UnityEngine.Random.Range(0, temp.Count - 1)];
            card.Init(upgrade, OnCardSelected);
            temp.Remove(upgrade);
        }
    }

    private void HandleUpgradeCard(UpgradeSO upgrade)
    {
        upgrade.AddEffect();
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

}
