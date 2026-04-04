using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade System")]
public abstract class UpgradeSO : ScriptableObject
{
    public Sprite icon;
    public string description;
    public float amount;
    public abstract void AddEffect();
}


