using UnityEngine;

public class PlayerSlideState : PlayerState
{
  

    // Chỉ thay đổi chiều cao khi trượt
    private float slideColliderHeight = 0.0338043f; // Chiều cao khi trượt
    private float slideColliderOffsetY = -0.216456f; // Độ lệch khi trượt (offset Y)

    // Tham chiếu đến CapsuleCollider2D
    private CapsuleCollider2D capsuleCollider;

    public override void Enter()
    {
        base.Enter();

        // Lấy Collider của nhân vật
        capsuleCollider = blackboard.GetComponent<CapsuleCollider2D>();

        if (capsuleCollider != null)
        {
            // Lưu kích thước và offset gốc
            blackboard.originalColliderSize = capsuleCollider.size;
            blackboard.originalColliderOffset = capsuleCollider.offset;

            // Đặt kích thước mới chỉ thay đổi theo trục Y
            capsuleCollider.size = new Vector2(blackboard.originalColliderSize.x, slideColliderHeight);

            // Đặt offset mới chỉ thay đổi theo trục Y
            capsuleCollider.offset = new Vector2(blackboard.originalColliderOffset.x, slideColliderOffsetY);
        }

        // Play animation Slide
        blackboard.animator.Play("Slide");

        // Đặt vận tốc trượt cho nhân vật
        blackboard.player.SetVelocityX(blackboard.playerData.SlideSpeed * blackboard.FacingDirection);
        startTime = blackboard.playerData.slideDuration; // Đặt thời gian slide từ playerData
        blackboard.playerData.UsageTimer = 0; // Đặt lại timer sau khi slide
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        startTime -= Time.deltaTime;

        // Use Slide input and block further slide input
        blackboard.PlayerInputHandler.UseSlideInput();

        // Check if the slide should end
        if (startTime <= 0 || blackboard.PlayerInputHandler.NormInputX == 0 || (!blackboard.isGrounded && Input.GetKeyUp(KeyCode.S)))
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
    }

    public override void Exit()
    {
        base.Exit();

        // Restore original collider size and offset when exiting the slide state
        if (capsuleCollider != null)
        {
            capsuleCollider.size = blackboard.originalColliderSize;
            capsuleCollider.offset = blackboard.originalColliderOffset;
        }
    }
    
}
