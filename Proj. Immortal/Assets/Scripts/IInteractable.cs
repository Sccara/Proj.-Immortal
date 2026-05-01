using UnityEngine;

public interface IInteractable 
{
    void Interact(PlayerManager player);
    string GetInteractText();
}
