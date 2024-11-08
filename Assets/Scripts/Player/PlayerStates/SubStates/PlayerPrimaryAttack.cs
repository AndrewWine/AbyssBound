using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    public override void Enter()
    {
        base.Enter();
       
        blackboard.animator.Play("Attack1");
        blackboard.playerData.CountClick++;
        isAnimationFinished = false;
        // Không reset CountClick ở đây
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
