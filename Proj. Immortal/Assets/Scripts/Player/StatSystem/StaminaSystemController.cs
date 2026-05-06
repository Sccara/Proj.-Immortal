using UnityEngine;

public class StaminaSystemController : MonoBehaviour
{
    [SerializeField] private float performActionCooldown;
    [SerializeField] private PlayerAttributes attributes;

    private float _regenTimer;

    private void Update()
    {
        if (_regenTimer > 0)
        {
            _regenTimer -= Time.deltaTime;
        }
        else if (attributes.StaminaResource.Percent < 1f)
        {
            attributes.StaminaResource.Restore(attributes.StaminaRestoreRateStat.Value * Time.deltaTime);
        }
    }

    public bool HasEnoughStamina() => attributes.StaminaResource.Current >= 1;

    public void UseStamina(float amount)
    {
        attributes.StaminaResource.Use(amount);
        _regenTimer = performActionCooldown;
    }
}