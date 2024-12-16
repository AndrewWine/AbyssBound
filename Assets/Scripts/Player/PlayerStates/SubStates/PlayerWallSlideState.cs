using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("Wall-Slide");
        blackboard.playerData.CurrentFacing = blackboard.FacingDirection ;
        blackboard.playerData.amountOfJump = 2;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (Input.GetKeyDown(KeyCode.Space))
        {
           
          stateMachine.ChangeState(blackboard.JumpState);
            
        }

        // Kiểm tra hướng quay mặt của nhân vật so với tường
        if ( !blackboard.isWall && blackboard.PlayerInputHandler.NormInputX == 0)
        {
          
                // Nhân vật quay hướng ngược với tường, chuyển sang trạng thái rơi
                stateMachine.ChangeState(blackboard.AirState);  // Hoặc FallState
                return;
        }

        else if (blackboard.PlayerInputHandler.NormInputX * blackboard.FacingDirection != 0 && blackboard.isWall)
        {
            // Nếu nhân vật vẫn dính vào tường, giảm tốc độ rơi
            blackboard.player.SetVelocityY(blackboard.RB.velocity.y * 0.8f);
            
        }

        if (!blackboard.isWall )
        {

            // Nhân vật quay hướng ngược với tường, chuyển sang trạng thái rơi
            stateMachine.ChangeState(blackboard.AirState);  // Hoặc FallState
            return;
        }
        // Kiểm tra nếu nhân vật chạm đất
        if (blackboard.isGrounded)
        {
            stateMachine.ChangeState(blackboard.idleState);
        }
    }
}
