using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ManaSystemController : MonoBehaviour
{
    [SerializeField] private PlayerAttributes attributes;

    public void UseMana(float amount)
    {
        if (attributes.ManaResource.Current <= 0)
            return;

        attributes.ManaResource.Use(amount);
    }

    public void RestoreMana(float amount)
    {
        attributes.ManaResource.Restore(amount);
    }

    public bool HasEnoughMana(float manaCost)
    {
        return attributes.ManaResource.Current >= manaCost;
    }
}
