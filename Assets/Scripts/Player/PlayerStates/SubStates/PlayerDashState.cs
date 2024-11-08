using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public override void Enter()
    {
        base.Enter();

        // Bắt đầu dash và khởi động lại cooldown
        blackboard.animator.Play("Dash");
        startTime = blackboard.playerData.dashDuration; // Đặt thời gian dash từ playerData
        blackboard.player.SetVelocityX(blackboard.playerData.dashSpeed * blackboard.FacingDirection);
        blackboard.player.SetVelocityY(0); // Ngăn nhân vật rơi trong khi dash (nếu cần)
        blackboard.playerData.UsageTimer = 0; // Đặt lại timer sau khi dash
      
    }


    public override void Exit()
    {
        base.Exit();
        // Đảm bảo sau khi kết thúc dash, người chơi phải chờ cooldown
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Giảm thời gian dash
        startTime -= Time.deltaTime;

        // Sử dụng DashInput và chặn dash tiếp theo
        blackboard.PlayerInputHandler.UseDashInput();

        // Kiểm tra nếu dash đã kết thúc
        if (startTime <= 0)
        {
            if (blackboard.RB.velocity.y < 0)
            {
                stateMachine.ChangeState(blackboard.AirState);
            }
            else
            { 
            stateMachine.ChangeState(blackboard.idleState); // Quay lại trạng thái idle khi dash kết thúc
            }
        }
    }


    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        // Điều chỉnh các thuộc tính vật lý khác nếu cần thiết trong quá trình dash
    }
}
