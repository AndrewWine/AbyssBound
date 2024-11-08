using UnityEngine;

public class PlayerCounterAttack : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("PlayerCounterAttack");
        startTime = blackboard.playerData.counterAttackDuration; // Đặt lại thời gian đếm ngược
        blackboard.PlayerInputHandler.RightClick = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Giảm dần thời gian theo Time.deltaTime
        startTime -= Time.deltaTime;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(blackboard.attackCheck.position, blackboard.playerData.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    startTime = 2; // Đặt thời gian đếm ngược lớn hơn 1 khi phản công thành công
                    blackboard.animator.Play("PlayerSucessfulCounterAttack");
                }
            }
        }

        // Kiểm tra điều kiện để chuyển về idleState
        if (startTime <= 0 || isAnimationFinished)
        {
            stateMachine.ChangeState(blackboard.idleState); // Chuyển sang idleState khi hết thời gian hoặc kết thúc hoạt ảnh
        }
    }
}
