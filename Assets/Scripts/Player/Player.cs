using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BlackBoard blackBoard;
    public PlayerState playerState;
    public PlayerStateMachine stateMachine;
    public PlayerData playerData;

    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;

    public void Awake()
    {
        // Initialize FacingDirection based on the current player rotation or default
        blackBoard.FacingDirection = transform.localScale.x > 0 ? 1 : -1;
    }

    public void Update()
    {
        CheckOnDrawGizmos();
        // Check if player should flip direction
        CheckIfShouldFlip(blackBoard.PlayerInputHandler.NormInputX);

        if (blackBoard.isGrounded)
        {
            // Reset jumps when grounded
            playerData.amountOfJump = 2;
        }
    }

    public void CheckOnDrawGizmos()
    {
        // Ground check
        blackBoard.isGrounded = Physics2D.Raycast(
            blackBoard.groundCheck.position,
            Vector2.down,
            playerData.groundCheckDistance,
            playerData.whatIsGround
        );
        Debug.Log("Is Grounded: " + blackBoard.isGrounded);

        // Wall check
        blackBoard.isWall = Physics2D.Raycast(
            blackBoard.wallCheck.position,
            Vector2.right * blackBoard.FacingDirection,
            playerData.WallCheckDistance,
            playerData.whatIsGround
        );
        Debug.Log("Is Wall: " + blackBoard.isWall + " FacingDirection: " + blackBoard.FacingDirection);
    }

    public void AnimationTrigger()
    {
        if (stateMachine != null && stateMachine.CurrentState != null)
        {
            stateMachine.CurrentState.AnimationFinishTrigger();
        }
    }

    // Improved flip logic
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != blackBoard.FacingDirection)
        {
            Flip();
        }
    }

    public virtual void Flip()
    {
        // Reverse FacingDirection
        blackBoard.FacingDirection *= -1;

        // Flip the player's local scale to mirror the sprite instead of using rotation
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        // Draw ground check ray
        Vector3 groundCheckPosition = blackBoard.groundCheck.position;
        Gizmos.DrawLine(groundCheckPosition, groundCheckPosition + Vector3.down * playerData.groundCheckDistance);

        // Draw wall check ray
        Vector3 wallCheckPosition = blackBoard.wallCheck.position;
        Gizmos.DrawLine(wallCheckPosition, wallCheckPosition + Vector3.right * blackBoard.FacingDirection * playerData.WallCheckDistance);
    }

    public virtual void SetVelocityZero()
    {
        blackBoard.RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public virtual void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        blackBoard.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public virtual void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        blackBoard.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
}
