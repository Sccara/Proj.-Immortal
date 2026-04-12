using UnityEngine;

public class PlayerStateFactory : MonoBehaviour
{
    PlayerStateMachine _context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(_context, this);
    }
    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(_context, this);
    }
    public PlayerBaseState Run()
    {
        return new PlayerRunState(_context, this);
    }
    public PlayerBaseState Staggered()
    { 
        return new PlayerStaggeredState(_context, this);
    }
    public PlayerBaseState LightAttack()
    {
        return new PlayerLightAttackState(_context, this);
    }
    public PlayerBaseState HeavyAttack()
    { 
        return new PlayerHeavyAttackState(_context, this);
    }
    public PlayerBaseState Dash()
    { 
        return new PlayerDashState(_context, this);
    }
    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(_context, this);
    }
    public PlayerBaseState Jump()
    {
        return new PlayerJumpState(_context, this);
    }
    public PlayerBaseState Fall()
    {
        return new PlayerFallState(_context, this);
    }
    public PlayerBaseState UseItem()
    {
        return new PlayerUseItemState(_context, this);
    }
}
