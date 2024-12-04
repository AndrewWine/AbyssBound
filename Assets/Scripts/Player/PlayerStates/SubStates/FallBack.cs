using UnityEngine;

public class FallBack : PlayerState
{
    public float duration;  

    public override void Enter()
    {
        duration = 1;  // Đặt thời gian kết thúc

        blackboard.animator.Play("FallBack");  // Chơi animation "FallBack"
        blackboard.player.SetVelocityX(blackboard.playerData.dashSpeed * 2 *  -blackboard.FacingDirection); 
        blackboard.player.SetVelocityY(0); // Ngăn nhân vật rơi trong khi dash
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        duration -= 0.1f;  // Giảm thời gian còn lại

        // Debug log để kiểm tra giá trị endtime
        Debug.Log("Endtime: " + duration);

        // Kiểm tra nếu thời gian đã hết
        if (duration <= 0f || blackboard.PlayerInputHandler.NormInputX != 0)
        {
            Debug.Log("Transitioning to Idle state.");
            stateMachine.ChangeState(blackboard.idleState);  // Chuyển sang trạng thái Idle sau 0.2 giây
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
