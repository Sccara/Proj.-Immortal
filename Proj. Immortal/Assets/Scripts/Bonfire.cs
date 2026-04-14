using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Bonfire : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;

    private void Start()
    {
        GetComponent<SphereCollider>().isTrigger = true;
    }

    private void RestAtBonfire()
    {
        Debug.Log("Rest");

        PlayerManager.Instance.Attributes.HealthResource.Restore(PlayerManager.Instance.Attributes.HealthResource.Max);
        PlayerManager.Instance.Attributes.StaminaResource.Restore(PlayerManager.Instance.Attributes.StaminaResource.Max);

        UIManager.Instance.ToggleWindow(WindowType.LevelUp, hideHUD: true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Убедись, что у игрока стоит тег "Player"
        {
            Debug.Log("Collision");
            // Начинаем слушать кнопку взаимодействия
            _inputReader.OnInteractPressed += RestAtBonfire;

            // Опционально: тут можно показать UI подсказку "Нажмите E для отдыха"
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Перестаем слушать кнопку
            _inputReader.OnInteractPressed -= RestAtBonfire;

            // Опционально: скрываем UI подсказку
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
