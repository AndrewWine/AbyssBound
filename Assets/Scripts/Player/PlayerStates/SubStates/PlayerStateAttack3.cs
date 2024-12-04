using System;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PlayerStateAttack3 : PlayerState
{
    public Action<int,Transform> PlaySfxAtk;
    public override void Enter()
    {
        base.Enter();
        blackboard.playerData.CountClick++;
        blackboard.animator.Play("Attack3"); // Phát animation Attack3
        PlaySfxAtk?.Invoke(36,null);
        blackboard.PlayerInputHandler.LeftClick = false; // Reset click input để chuẩn bị cho combo tiếp theo
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Kiểm tra nếu animation đã hoàn thành và đã hoàn tất combo
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
