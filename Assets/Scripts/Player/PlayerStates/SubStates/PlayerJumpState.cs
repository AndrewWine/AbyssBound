using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        // Perform jump when entering Jump state
        PerformJump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Check if player is grounded again
      
        if (blackboard.isGrounded && isAnimationFinished)
        {
            // Reset jump count when grounded (2 for double jump)

            // Change state to Idle or Move based on input
            if (blackboard.PlayerInputHandler.NormInputX != 0)
            {
                stateMachine.ChangeState(blackboard.MoveState);
            }
            else
            {
                stateMachine.ChangeState(blackboard.idleState);
            }
        }
        else
        {
            // If falling, switch to Air state
            if (blackboard.RB.velocity.y < 0)
            {
                stateMachine.ChangeState(blackboard.AirState);
            }
        }

        // Check if Dash input is pressed
        if (blackboard.PlayerInputHandler.DashInput)
        {
            stateMachine.ChangeState(blackboard.DashState);
        }
    }

    private void PerformJump()
    {
        if (blackboard.playerData.amountOfJump > 0)
        {
            // Reduce the jump count
            blackboard.playerData.amountOfJump--;

            // Play jump animation
            blackboard.animator.Play("jump");

            // Apply vertical jump velocity
            blackboard.player.SetVelocityY(blackboard.playerData.jumpVelocity);

            // Handle wall jump with special velocity changes
            if (blackboard.wallCheck)
            {
                blackboard.player.SetVelocityX(5 * blackboard.PlayerInputHandler.NormInputX);
                blackboard.player.SetVelocityY(blackboard.playerData.jumpVelocity );

            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        // Update falling velocity if necessary
        if (blackboard.RB.velocity.y < 0)
        {
            blackboard.player.SetVelocityY(blackboard.RB.velocity.y);
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }
}
