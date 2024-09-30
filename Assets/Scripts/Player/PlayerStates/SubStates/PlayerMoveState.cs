using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        // Chạy animation Run
        if(blackBoard.isGrounded)
        {
            blackBoard.animator.Play("Run");
        }
        player.SetVelocityY(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Chuyển sang trạng thái Idle nếu không còn đầu vào di chuyển
        if (blackBoard.PlayerInputHandler.NormInputX == 0 && blackBoard.isGrounded)
        {
            stateMachine.ChangeState(blackBoard.idleState);
        }

        // Chuyển sang trạng thái Jump nếu nhấn phím nhảy
        if (blackBoard.PlayerInputHandler.JumpInput)
        {
            stateMachine.ChangeState(blackBoard.JumpState);
        }

        // Chuyển sang trạng thái Dash nếu điều kiện được thỏa mãn
        if (blackBoard.PlayerInputHandler.DashInput )
        {
            stateMachine.ChangeState(blackBoard.DashState);
        }

        if( !blackBoard.isGrounded)
        {
            stateMachine.ChangeState(blackBoard.AirState);
        }

        if(blackBoard.PlayerInputHandler.SlideInput)
        {
            stateMachine.ChangeState(blackBoard.SlideState);

        }
        if (blackBoard.PlayerInputHandler.LeftClick)
        {
            if (playerData.CountClick == 2)
            {
                stateMachine.ChangeState(blackBoard.runattack2);
            }
            if (playerData.CountClick == 1)
            {
                stateMachine.ChangeState(blackBoard.runattack1);

            }

        }

    }


    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        // Cập nhật vận tốc dựa trên đầu vào và dữ liệu từ PlayerData

        float moveInput = blackBoard.PlayerInputHandler.NormInputX;
        player.SetVelocityX(moveInput  * playerData.movementSpeed); // Sử dụng thuộc tính movementVelocity
    }
}
