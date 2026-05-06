using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Souls Event Channel")]
public class SoulsEventChannelSO : ScriptableObject
{
    public Action<float> OnSoulsDropped;

    public void RaiseEvent(float amount)
    {
        OnSoulsDropped?.Invoke(amount);
    }
}
