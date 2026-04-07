using UnityEngine;

public class ExperienceSphere : MonoBehaviour
{
    [SerializeField] private float expAmount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out LevelSystemController playerLevel))
        {
            playerLevel.AddSouls(expAmount);
            Destroy(gameObject);
        }
    }
}
