using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private float interactRange = 3f;
    private bool _isPlayerInRange;
    [SerializeField] private InputReader _inputReader;



    private void Update()
    {
        float distance = Vector3.Distance(transform.position, PlayerManager.Instance.transform.position);

        if (distance <= interactRange && _inputReader.InteractPressed)
        {
            RestAtBonfire();
        }
    }

    private void RestAtBonfire()
    {
        PlayerStats.Instance.Health.Restore(PlayerStats.Instance.Health.Max);
        PlayerStats.Instance.Stamina.Restore(PlayerStats.Instance.Stamina.Max);

        PlayerManager.Instance.Level.ShowUpgradePanel();
    }
}
