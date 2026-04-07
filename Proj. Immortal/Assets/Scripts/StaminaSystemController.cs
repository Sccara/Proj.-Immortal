using System.Collections;
using UnityEngine;

public class StaminaSystemController : MonoBehaviour
{
    [SerializeField] private float performActionCooldown;

    private bool _isRegenPaused;

    private void Update()
    {
        if (PlayerStats.Instance.Stamina.Percent < 1f && !_isRegenPaused)
        {
            PlayerStats.Instance.Stamina.Restore(PlayerStats.Instance.StaminaRestoreRate * Time.deltaTime);
        }
    }

    public bool HasEnoughStamina() => PlayerStats.Instance.Stamina.Current >= 1;

    public void UseStamina(float amount)
    {
        PlayerStats.Instance.Stamina.Use(amount);
        StartCoroutine(PauseRegenCoroutine());
    }

    private IEnumerator PauseRegenCoroutine()
    {
        _isRegenPaused = true;
        yield return new WaitForSeconds(performActionCooldown);
        _isRegenPaused = false;
    }
}
