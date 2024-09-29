using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        blackBoard.animator.Play("Wall-Slide");
        playerData.CurrentFacing = blackBoard.FacingDirection ;
        playerData.amountOfJump = 1;

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();


        if (Input.GetKeyDown(KeyCode.Space))
        {
           
          stateMachine.ChangeState(blackBoard.JumpState);
            
        }

        // Kiểm tra hướng quay mặt của nhân vật so với tường
        if ( !blackBoard.isWall && blackBoard.PlayerInputHandler.NormInputX == 0)
        {
          
                // Nhân vật quay hướng ngược với tường, chuyển sang trạng thái rơi
                stateMachine.ChangeState(blackBoard.AirState);  // Hoặc FallState
                return;
        }

        else if (blackBoard.PlayerInputHandler.NormInputX * blackBoard.FacingDirection != 0 && blackBoard.isWall)
        {
            // Nếu nhân vật vẫn dính vào tường, giảm tốc độ rơi
            player.SetVelocityY(blackBoard.RB.velocity.y * 0.8f);
            
        }

        if (!blackBoard.isWall )
        {

            // Nhân vật quay hướng ngược với tường, chuyển sang trạng thái rơi
            stateMachine.ChangeState(blackBoard.AirState);  // Hoặc FallState
            return;
        }
        // Kiểm tra nếu nhân vật chạm đất
        if (blackBoard.isGrounded)
        {
            stateMachine.ChangeState(blackBoard.idleState);
        }
    }
}
