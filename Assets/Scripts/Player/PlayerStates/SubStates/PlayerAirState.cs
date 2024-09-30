using UnityEngine;

public class PlayerAirState : PlayerState
{
    public override void Enter()
    {
        base.Enter();

        // Chơi animation "Fall" khi vào trạng thái trên không
        if (blackBoard.animator != null)
        {
            blackBoard.animator.Play("Fall");
        }
        else
        {
            Debug.LogError("Animator is not assigned in BlackBoard!");
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Kiểm tra nếu nhân vật đã chạm đất
        if (blackBoard.isGrounded)
        {
            if (blackBoard.PlayerInputHandler.NormInputX != 0)
            {
                stateMachine.ChangeState(blackBoard.MoveState);
            }
            else
            {
                stateMachine.ChangeState(blackBoard.idleState);
            }
        }
        if (blackBoard.PlayerInputHandler.JumpInput && playerData.amountOfJump > 0)
        {
            // Nếu người chơi có input nhảy và còn lượt nhảy, thực hiện double jump
            stateMachine.ChangeState(blackBoard.JumpState);
        }
        
        if( blackBoard.JumpInput && blackBoard.isWall)
        {
            stateMachine.ChangeState(blackBoard.WallJumpState);

        }
        // Chuyển sang trạng thái Dash nếu điều kiện được thỏa mãn
        if (blackBoard.PlayerInputHandler.DashInput )
        {
            stateMachine.ChangeState(blackBoard.DashState);
        }

        if (blackBoard.isWall)
        {
            stateMachine.ChangeState(blackBoard.WallSlideState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        // Cập nhật các thuộc tính vật lý liên quan đến trạng thái trên không nếu cần thiết
    }
}
