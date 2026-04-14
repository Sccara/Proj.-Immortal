using System;
using System.Collections;
using UnityEngine;

public class StaminaSystemController : MonoBehaviour
{
    [SerializeField] private float performActionCooldown;

    private bool _isRegenPaused;

    private void Update()
    {

        if (PlayerManager.Instance.Attributes.StaminaResource.Percent < 1f && !_isRegenPaused)
        {
            PlayerManager.Instance.Attributes.StaminaResource.Restore(PlayerManager.Instance.Attributes.StaminaRestoreRateStat.Value * Time.deltaTime);
        }
    }

    public bool HasEnoughStamina() => PlayerManager.Instance.Attributes.StaminaResource.Current >= 1;

    public void UseStamina(float amount)
    {
        PlayerManager.Instance.Attributes.StaminaResource.Use(amount);
        StartCoroutine(PauseRegenCoroutine());
    }

    private IEnumerator PauseRegenCoroutine()
    {
        _isRegenPaused = true;
        yield return new WaitForSeconds(performActionCooldown);
        _isRegenPaused = false;
    }
}
