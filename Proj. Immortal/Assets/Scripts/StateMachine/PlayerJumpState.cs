using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory)
    { 
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Jump state");

        Vector3 velocity = Ctx.Rb.linearVelocity;
        velocity.y = 0;
        Ctx.Rb.linearVelocity = velocity;

        // Применяем силу прыжка
        Ctx.Rb.AddForce(Vector3.up * Ctx.Config.jumpForce, ForceMode.Impulse);

        // Тратим стамину
        Ctx.PlayerManager.Stamina.UseStamina(Ctx.Config.jumpStamina);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void FixedUpdateState()
    {
        HandleAirPhysics();
    }
    public override void ExitState()
    {

    }
    public override void InitializeSubState()
    {

    }
    public override void CheckSwitchStates()
    {
        if (Ctx.Rb.linearVelocity.y < 0)
        {
            SwitchState(Factory.Fall());
        }
    }

    private void HandleAirPhysics()
    {
        Vector3 direction = Ctx.GetMoveDirection();

        // 1. Максимальная скорость зависит от того, КАК мы прыгнули, а не от того, что мы жмем сейчас
        float maxAirSpeed = Ctx.IsSprintJump ? Ctx.Config.sprintMoveSpeed : Ctx.Config.walkMoveSpeed;

        // 2. Рулежка в воздухе (если игрок жмет WASD)
        Ctx.Rb.AddForce(direction * maxAirSpeed * 0.5f, ForceMode.Acceleration);

        // 3. Срез инерции (Clamp)
        Vector3 currentVelocity = Ctx.Rb.linearVelocity;
        Vector3 horizontalVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);

        // Теперь, если мы прыгнули со спринта, лимит - SprintSpeed. Мы пролетим далеко!
        // И даже если отпустить Shift в воздухе, скорость не срежется до WalkSpeed.
        if (horizontalVelocity.magnitude > maxAirSpeed)
        {
            Vector3 clampedVelocity = horizontalVelocity.normalized * maxAirSpeed;
            Ctx.Rb.linearVelocity = new Vector3(clampedVelocity.x, currentVelocity.y, clampedVelocity.z);
        }
    }
}
