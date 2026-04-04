using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }
}
