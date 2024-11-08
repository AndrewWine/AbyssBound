using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        // Chạy animation Run
        if(blackboard.isGrounded)
        {
            blackboard.animator.Play("Run");
        }
        blackboard.player.SetVelocityY(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Chuyển sang trạng thái Idle nếu không còn đầu vào di chuyển
        if (blackboard.PlayerInputHandler.NormInputX == 0 && blackboard.isGrounded)
        {
            stateMachine.ChangeState(blackboard.idleState);
        }

        // Chuyển sang trạng thái Jump nếu nhấn phím nhảy
        if (blackboard.PlayerInputHandler.JumpInput)
        {
            stateMachine.ChangeState(blackboard.JumpState);
        }

        // Chuyển sang trạng thái Dash nếu điều kiện được thỏa mãn
        if (blackboard.PlayerInputHandler.DashInput )
        {
            stateMachine.ChangeState(blackboard.DashState);
        }

        if( !blackboard.isGrounded)
        {
            stateMachine.ChangeState(blackboard.AirState);
        }

        if(blackboard.PlayerInputHandler.SlideInput)
        {
            stateMachine.ChangeState(blackboard.SlideState);

        }
        if (blackboard.PlayerInputHandler.LeftClick)
        {
            if (blackboard.playerData.CountClick % 2 == 0)
            {
                stateMachine.ChangeState(blackboard.runattack2);
            }
            else 
            {
                stateMachine.ChangeState(blackboard.runattack1);

            }

        }

    }


    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        // Cập nhật vận tốc dựa trên đầu vào và dữ liệu từ PlayerData

        float moveInput = blackboard.PlayerInputHandler.NormInputX;
        blackboard.player.SetVelocityX(moveInput  * blackboard.playerData.movementSpeed); // Sử dụng thuộc tính movementVelocity
    }
}
