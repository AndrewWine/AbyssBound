 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(10 * -blackBoard.FacingDirection);
        player.SetVelocityY(playerData.jumpVelocity * 2f);
        blackBoard.animator.Play("jump");
        playerData.amountOfJump --;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if ( !blackBoard.isWall && !blackBoard.isGrounded )
        {
            stateMachine.ChangeState(blackBoard.AirState);
        }

        if(blackBoard.isGrounded )
        {
            stateMachine.ChangeState(blackBoard.idleState);

        }
    }
    
 
}
