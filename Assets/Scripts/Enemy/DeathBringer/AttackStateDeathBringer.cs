using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateDeathBringer : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("Attack");
        blackboard.enemy.countAttack += 1;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Chỉ thay đổi trạng thái nếu animation đã kết thúc
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(blackboard.enemyDBWalkState);
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAnimationFinished = true; // Đánh dấu rằng animation đã kết thúc
    }

    public override void Exit()
    {
        base.Exit();
        blackboard.enemyData.lastTimeAttacked = Time.time; // Cập nhật thời gian tấn công gần nhất
    }
}
