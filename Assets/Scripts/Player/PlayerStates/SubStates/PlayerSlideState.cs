using UnityEngine;

public class PlayerSlideState : PlayerState
{
  

    // Chỉ thay đổi chiều cao khi trượt
    private float slideColliderHeight = 0.2338043f; // Chiều cao khi trượt
    private float slideColliderOffsetY = -0.216456f; // Độ lệch khi trượt (offset Y)

    // Tham chiếu đến CapsuleCollider2D
    private CapsuleCollider2D capsuleCollider;

    public override void Enter()
    {
        base.Enter();

        // Lấy Collider của nhân vật
        capsuleCollider = blackBoard.GetComponent<CapsuleCollider2D>();

        if (capsuleCollider != null)
        {
            // Lưu kích thước và offset gốc
            blackBoard.originalColliderSize = capsuleCollider.size;
            blackBoard.originalColliderOffset = capsuleCollider.offset;

            // Đặt kích thước mới chỉ thay đổi theo trục Y
            capsuleCollider.size = new Vector2(blackBoard.originalColliderSize.x, slideColliderHeight);

            // Đặt offset mới chỉ thay đổi theo trục Y
            capsuleCollider.offset = new Vector2(blackBoard.originalColliderOffset.x, slideColliderOffsetY);
        }

        // Play animation Slide
        blackBoard.animator.Play("Slide");

        // Đặt vận tốc trượt cho nhân vật
        player.SetVelocityX(playerData.SlideSpeed * blackBoard.FacingDirection);
        startTime = playerData.slideDuration; // Đặt thời gian slide từ playerData
        playerData.UsageTimer = 0; // Đặt lại timer sau khi slide
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        startTime -= Time.deltaTime;

        // Use Slide input and block further slide input
        blackBoard.PlayerInputHandler.UseSlideInput();

        // Check if the slide should end
        if (startTime <= 0 || blackBoard.PlayerInputHandler.NormInputX == 0 || (!blackBoard.isGrounded && Input.GetKeyUp(KeyCode.S)))
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
    }

    public override void Exit()
    {
        base.Exit();

        // Restore original collider size and offset when exiting the slide state
        if (capsuleCollider != null)
        {
            capsuleCollider.size = blackBoard.originalColliderSize;
            capsuleCollider.offset = blackBoard.originalColliderOffset;
        }
    }
    
}
