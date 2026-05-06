using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Bonfire : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayerAttributes playerAttributes;

    private void Start()
    {
        GetComponent<SphereCollider>().isTrigger = true;
    }

    private void RestAtBonfire()
    {
        Debug.Log("Rest");

        playerAttributes.HealthResource.Restore(playerAttributes.HealthResource.Max);
        playerAttributes.StaminaResource.Restore(playerAttributes.StaminaResource.Max);
        playerAttributes.ManaResource.Restore(playerAttributes.ManaResource.Max);


        UIManager.Instance.ToggleWindow(WindowType.LevelUp, hideHUD: true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inputReader.OnInteractPressed += RestAtBonfire;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inputReader.OnInteractPressed -= RestAtBonfire;
        }
    }

    private void OnDisable()
    {
        if (_inputReader != null)
        {
            _inputReader.OnInteractPressed -= RestAtBonfire;
        }
    }
}
