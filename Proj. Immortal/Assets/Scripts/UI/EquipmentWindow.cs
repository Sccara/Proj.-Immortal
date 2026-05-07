using System.Collections.Generic;
using UnityEngine;

public class EquipmentWindow : UIWindow
{
    [SerializeField] private PlayerEquipment equipment;
    [SerializeField] private Inventory inventory;
    [SerializeField] private UIManager uiManager;

    public override void OnOpen()
    {
        base.OnOpen();
    }
}
