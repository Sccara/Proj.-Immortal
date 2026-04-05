using System.Collections;
using UnityEngine;

public class StaminaSystemController : MonoBehaviour
{
    [SerializeField] StaminaSystem _staminaSystem;
    [SerializeField] StaminaSystemUI _staminaSystemUI;

    [SerializeField] private bool _isPerformedAction;
    [SerializeField] private float performActionCooldown;

    public bool IsPerformedAction {  get { return _isPerformedAction; } set { _isPerformedAction = value; } }

    private void Start()
    {
        _staminaSystem = new StaminaSystem(100, 100);
        _staminaSystemUI.UpdateStaminaBar(_staminaSystem.StaminaPercent);
    }

    private void Update()
    {
        if (_staminaSystem.StaminaPercent < 1 && !_isPerformedAction)
        {
            RestoreStamina(PlayerStats.Instance.StaminaRestoreRate * Time.deltaTime);
            _staminaSystemUI.UpdateStaminaBar(_staminaSystem.StaminaPercent);
        }
    }

    private IEnumerator PerformAction()
    {
        _isPerformedAction = true;

        yield return new WaitForSeconds(performActionCooldown);

        _isPerformedAction = false;
    }

    public bool CheckStamina()
    {
        return _staminaSystem.Stamina > 0;
    }

    public void UseStamina(float amount)
    {
        _staminaSystem.UseStamina(amount);
        StartCoroutine(PerformAction());
        _staminaSystemUI.UpdateStaminaBar(_staminaSystem.StaminaPercent);
    }

    public void RestoreStamina(float amount)
    {
        _staminaSystem.RestoreStamina(amount);
    }
}
