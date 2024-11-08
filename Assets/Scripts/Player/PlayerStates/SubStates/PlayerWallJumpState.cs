 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        blackboard.player.SetVelocityX(10 * -blackboard.FacingDirection);
        blackboard.player.SetVelocityY(blackboard.playerData.jumpVelocity * 2f);
        blackboard.animator.Play("jump");
        blackboard.playerData.amountOfJump --;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if ( !blackboard.isWall && !blackboard.isGrounded )
        {
            stateMachine.ChangeState(blackboard.AirState);
        }

        if(blackboard.isGrounded )
        {
            stateMachine.ChangeState(blackboard.idleState);

        }
    }
    
 
}
