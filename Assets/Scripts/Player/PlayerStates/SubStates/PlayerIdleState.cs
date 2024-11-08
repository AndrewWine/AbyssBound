using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        // Đặt vận tốc về 0 khi đang đứng yên
        blackboard.player.SetVelocityZero();
        blackboard.player.SetVelocityX(0);
        blackboard.player.SetVelocityY(0);
        blackboard.animator.Play("Idle"); // Chạy animation Idle
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (blackboard.isGrounded)
        {
            if (blackboard.PlayerInputHandler.NormInputX != 0)
            {
                stateMachine.ChangeState(blackboard.MoveState);
            }

            
            if (blackboard.PlayerInputHandler.JumpInput)
            {
                stateMachine.ChangeState(blackboard.JumpState);
            }
        }
      
        if (blackboard.PlayerInputHandler.DashInput)
        {
            stateMachine.ChangeState(blackboard.DashState);
        }

        if(blackboard.PlayerInputHandler.LeftClick)
        {
            if (blackboard.playerData.CountClick == 1)
                stateMachine.ChangeState(blackboard.primaryAttack);
            else if (blackboard.playerData.CountClick == 2)
                stateMachine.ChangeState(blackboard.primaryAttack2);
            else if (blackboard.playerData.CountClick == 3)
                stateMachine.ChangeState(blackboard.primaryAttack3);
        }

        if(blackboard.PlayerInputHandler.RightClick)
        {
            stateMachine.ChangeState(blackboard.counterattack);

        }

        if(blackboard.PlayerInputHandler.PressedKeyQ)
        {
            stateMachine.ChangeState(blackboard.aimSword);
        }    
        
    }

}
