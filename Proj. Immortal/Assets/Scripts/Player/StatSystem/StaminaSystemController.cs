using System;
using System.Collections;
using UnityEngine;

public class StaminaSystemController : MonoBehaviour
{
    [SerializeField] private float performActionCooldown;
    [SerializeField] private PlayerAttributes attributes;

    private bool _isRegenPaused;

    private void Update()
    {

        if (attributes.StaminaResource.Percent < 1f && !_isRegenPaused)
        {
            attributes.StaminaResource.Restore(attributes.StaminaRestoreRateStat.Value * Time.deltaTime);
        }
    }

    public bool HasEnoughStamina() => attributes.StaminaResource.Current >= 1;

    public void UseStamina(float amount)
    {
        attributes.StaminaResource.Use(amount);
        StartCoroutine(PauseRegenCoroutine());
    }

    private IEnumerator PauseRegenCoroutine()
    {
        _isRegenPaused = true;
        yield return new WaitForSeconds(performActionCooldown);
        _isRegenPaused = false;
    }
}
