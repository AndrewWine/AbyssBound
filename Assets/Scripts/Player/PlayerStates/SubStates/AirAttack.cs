using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirAttack : PlayerState
{
   
    public override void Enter()
    {
        blackboard.animator.Play("AirAttack");
        isAnimationFinished = false;
        blackboard.PlayerInputHandler.LeftClick = false; // Reset click input để chuẩn bị cho combo tiếp theo
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {

        blackboard.player.SetVelocityY(blackboard.RB.velocity.y * 1.01f);

        if (blackboard.isGrounded)
        {
            if (blackboard.PlayerInputHandler.NormInputX != 0)
            {
                stateMachine.ChangeState(blackboard.MoveState);
            }
            else
            {
                stateMachine.ChangeState(blackboard.idleState);
            }
        isAnimationFinished = true; // Đánh dấu rằng animation đã kết thúc
        }
        if (blackboard.isWall)
        {
            stateMachine.ChangeState(blackboard.WallSlideState);
            isAnimationFinished = true; // Đánh dấu rằng animation đã kết thúc

        }
        base.LogicUpdate();
    }


    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
      
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
     
    }
}
