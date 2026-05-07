using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState()
    {
        Debug.Log("Grounded state");
        Ctx.Animator.CrossFadeInFixedTime("Movement", 0.1f, 0, 0f);
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {

    }
    public override void ExitState()
    {

    }
    public override void InitializeSubState()
    {
        if (!Ctx.Input.IsMovementPressed)
        {
            SetSubState(Factory.Idle());
        }
        else
        {
            SetSubState(Factory.Walk());
        }
    }
    public override void CheckSwitchStates()
    {

    }

    public override bool HandleInput(InputCommand command)
    {
        Debug.Log($"Command: {command}");

        switch (command)
        {
            case InputCommand.Dash:
                if (Ctx.DashCooldownTimer <= 0)
                {
                    SwitchState(Factory.Dash());
                    return true; 
                }
                break;
            case InputCommand.Jump:
                if (Ctx.PlayerManager.Stamina.HasEnoughStamina())
                {
                    Ctx.IsSprintJump = Ctx.Input.IsSprinting && !Ctx.IsSprintBroken;
                    SwitchState(Factory.Jump());
                    return true;
                }
                break;
            case InputCommand.LightAttack:
                ExecuteRightHandAction();
                return true;
            case InputCommand.HeavyAttack:
                SwitchState(Factory.HeavyAttack());
                return true;
            case InputCommand.UseItem:
                ItemInstance activeItem = Ctx.PlayerManager.QuickItems.CurrentItem;
                int quantity = Ctx.PlayerManager.QuickItems.GetCurrentItemQuantity();

                if (activeItem != null && quantity > 0)
                {
                    SwitchState(Factory.UseItem());
                }     
                else
                {
                    // SwitchState(Factory.CantUseItem()); // TO DO
                }
                return true;
        }

        return base.HandleInput(command);
    }

    private void ExecuteRightHandAction()
    {
        WeaponInstance rightWeapon = Ctx.PlayerManager.Equipment.RightHand;

        if (rightWeapon.WeaponData == null)
        {
            SwitchState(Factory.LightAttack());
            return;
        }

        WeaponSO weaponData = rightWeapon.WeaponData;

        switch (weaponData.Class)
        {
            case WeaponClass.Melee:
                SwitchState(Factory.LightAttack());
                break;
            case WeaponClass.Catalyst:
                SpellInstance activeSpell = Ctx.PlayerManager.SpellMemory.CurrentSpell;

                if (activeSpell != null)
                {
                    SwitchState(Factory.Cast());
                }
                else
                { 
                    
                    SwitchState(Factory.LightAttack());
                }
                break;

            case WeaponClass.Ranged:
                // SwitchState(Factory.AimRangedWeapon());
                SwitchState(Factory.LightAttack());
                break;
        }
    }
}
