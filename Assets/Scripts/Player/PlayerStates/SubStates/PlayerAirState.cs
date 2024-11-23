using UnityEngine;

public class PlayerAirState : PlayerState
{
    public override void Enter()
    {
        base.Enter();

        // Chơi animation "Fall" khi vào trạng thái trên không
        if (blackboard.animator != null)
        {
            blackboard.animator.Play("Fall");
        }
        else
        {
            Debug.LogError("Animator is not assigned in Playerblackboard!");
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Kiểm tra nếu nhân vật đã chạm đất
        if (blackboard.isGrounded)
        {
            if (blackboard.PlayerInputHandler.NormInputX != 0)
            {
                stateMachine.ChangeState(blackboard.MoveState);
            }
            else
            {
                stateMachine.ChangeState(blackboard.idleState);
            }

        }
        if (blackboard.PlayerInputHandler.JumpInput && blackboard.playerData.amountOfJump > 0)
        {
            // Nếu người chơi có input nhảy và còn lượt nhảy, thực hiện double jump
            stateMachine.ChangeState(blackboard.JumpState);
        }
        
        if( blackboard.JumpInput && blackboard.isWall)
        {
            stateMachine.ChangeState(blackboard.WallJumpState);

        }
        // Chuyển sang trạng thái Dash nếu điều kiện được thỏa mãn
        if (blackboard.PlayerInputHandler.DashInput )
        {
            stateMachine.ChangeState(blackboard.DashState);
        }

        if (blackboard.isWall)
        {
            stateMachine.ChangeState(blackboard.WallSlideState);
        }
        if(blackboard.PlayerInputHandler.LeftClick)
        {
            stateMachine.ChangeState(blackboard.playerairAttack);

        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        // Cập nhật các thuộc tính vật lý liên quan đến trạng thái trên không nếu cần thiết
    }
}
