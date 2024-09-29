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
      
        if (blackBoard.isGrounded)
        {
            // Reset jump count when grounded (2 for double jump)
            playerData.amountOfJump = 1;

            // Change state to Idle or Move based on input
            if (blackBoard.PlayerInputHandler.NormInputX != 0)
            {
                stateMachine.ChangeState(blackBoard.MoveState);
            }
            else
            {
                stateMachine.ChangeState(blackBoard.idleState);
            }
        }
        else
        {
            // If falling, switch to Air state
            if (blackBoard.RB.velocity.y < 0)
            {
                stateMachine.ChangeState(blackBoard.AirState);
            }
        }

        // Check if Dash input is pressed
        if (blackBoard.PlayerInputHandler.DashInput)
        {
            stateMachine.ChangeState(blackBoard.DashState);
        }
    }

    private void PerformJump()
    {
        if (playerData.amountOfJump > 0)
        {
            // Reduce the jump count
            playerData.amountOfJump--;

            // Play jump animation
            blackBoard.animator.Play("jump");

            // Apply vertical jump velocity
            player.SetVelocityY(playerData.jumpVelocity);

            // Handle wall jump with special velocity changes
            if (blackBoard.wallCheck)
            {
                player.SetVelocityX(5 * blackBoard.PlayerInputHandler.NormInputX);
                player.SetVelocityY(playerData.jumpVelocity );

            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        // Update falling velocity if necessary
        if (player.blackBoard.RB.velocity.y < 0)
        {
            player.SetVelocityY(player.blackBoard.RB.velocity.y);
        }
    }
}
