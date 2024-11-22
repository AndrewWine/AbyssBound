using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : EnemyState
{
    private float remainingStunTime;

    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("Stun");
        blackboard.enemy.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);
        remainingStunTime = blackboard.stunDuration;

        // Đặt vận tốc stun
        blackboard.RB.velocity = Vector2.zero;
        blackboard.RB.velocity = new Vector2(-blackboard.FacingDirection * blackboard.stunDirection.x, blackboard.stunDirection.y);
    }

    public override void LogicUpdate()
    {
        remainingStunTime -= Time.deltaTime;
        base.LogicUpdate();
        if (remainingStunTime <= 0)
        {
            stateMachine.ChangeState(blackboard.enemyIdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        blackboard.enemy.fx.CancelInvoke("RedColorBlink"); // Dừng hiệu ứng màu đỏ
        blackboard.enemy.fx.Invoke("CancelRedBlink", 0); // Hủy nhấp nháy
        blackboard.isKnocked = false; // Reset trạng thái knock
    }
}
