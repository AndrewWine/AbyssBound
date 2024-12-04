using System;
using UnityEngine;

public class PlayerStateAttack2 : PlayerState
{
    public Action<int, Transform> PlaySfxAtk; 
    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("Attack2");
        PlaySfxAtk?.Invoke(36, null);
        blackboard.playerData.CountClick++;
        blackboard.PlayerInputHandler.LeftClick = false; // Reset click input để chuẩn bị cho combo tiếp theo
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
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

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAnimationFinished = true; // Đánh dấu rằng animation đã kết thúc
    }

}
