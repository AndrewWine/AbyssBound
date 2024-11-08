using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAttack1 : PlayerState
{


    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("RunAttack1"); // Phát animation Attack3
        isAnimationFinished = false; // Đặt lại biến isAnimationFinished
        blackboard.PlayerInputHandler.LeftClick = false; // Reset click input để chuẩn bị cho combo tiếp theo
        blackboard.playerData.CountClick++;

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {
            if (isAnimationFinished)
            {

                if (blackboard.PlayerInputHandler.NormInputX != 0 && blackboard.isGrounded)
                {
                    stateMachine.ChangeState(blackboard.MoveState);
                }
                else if (blackboard.PlayerInputHandler.DashInput)
                {
                    stateMachine.ChangeState(blackboard.DashState);
                }
                else if (blackboard.PlayerInputHandler.JumpInput)
                {
                    stateMachine.ChangeState(blackboard.JumpState);
                }
                else
                {
                    stateMachine.ChangeState(blackboard.idleState);
                }
            }
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAnimationFinished = true; // Đánh dấu rằng animation đã kết thúc
    }
}
