using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        // Đặt vận tốc về 0 khi đang đứng yên
        player.SetVelocityZero();
        player.SetVelocityX(0);
        player.SetVelocityY(0);
        blackBoard.animator.Play("Idle"); // Chạy animation Idle
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(blackBoard.isGrounded)
        {
            if (blackBoard.PlayerInputHandler.NormInputX != 0)
            {
                stateMachine.ChangeState(blackBoard.MoveState);
            }

            
            if (blackBoard.PlayerInputHandler.JumpInput)
            {
                stateMachine.ChangeState(blackBoard.JumpState);
            }
        }
      
        if (blackBoard.PlayerInputHandler.DashInput )
        {
            stateMachine.ChangeState(blackBoard.DashState);
        }

        if(blackBoard.PlayerInputHandler.LeftClick)
        {
            if(playerData.CountClick == 1)
                stateMachine.ChangeState(blackBoard.primaryAttack);

            else if( playerData.CountClick == 2)
                stateMachine.ChangeState(blackBoard.primaryAttack2);
            else if (playerData.CountClick == 3)
                stateMachine.ChangeState(blackBoard.primaryAttack3);
        }
    }

}
