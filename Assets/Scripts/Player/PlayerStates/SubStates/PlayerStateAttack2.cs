using UnityEngine;

public class PlayerStateAttack2 : PlayerState
{

    public override void Enter()
    {
        base.Enter();
        blackBoard.animator.Play("Attack2");
        isAnimationFinished = false;
        blackBoard.PlayerInputHandler.LeftClick = false; // Reset click input để chuẩn bị cho combo tiếp theo
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
        {

            if (blackBoard.PlayerInputHandler.NormInputX != 0 && blackBoard.isGrounded)
            {
                stateMachine.ChangeState(blackBoard.MoveState);
            }
            else if (blackBoard.PlayerInputHandler.DashInput)
            {
                stateMachine.ChangeState(blackBoard.DashState);
            }
            else if (blackBoard.PlayerInputHandler.JumpInput)
            {
                stateMachine.ChangeState(blackBoard.JumpState);
            }
            else
            {
                stateMachine.ChangeState(blackBoard.idleState);
            }
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAnimationFinished = true; // Đánh dấu rằng animation đã kết thúc
    }

}
