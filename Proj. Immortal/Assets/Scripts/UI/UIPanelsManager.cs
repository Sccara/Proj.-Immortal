using UnityEngine;

public class UIPanelsManager : MonoBehaviour
{
    [SerializeField] private GameObject loseScreen;

    private void Awake()
    {
        HealthSystemController.OnDeath += ShowLoseScreen;
    }

    public void ShowLoseScreen()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        loseScreen.SetActive(true);
    }
}
