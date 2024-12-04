using UnityEngine;
    public class AttackState : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("Attack");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

       
        // Chỉ thay đổi trạng thái nếu animation đã kết thúc
        if (isAnimationFinished)
        {
            stateMachine.ChangeState(blackboard.enemyWalkState);
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
        blackboard.enemyData.lastTimeAttacked = Time.time;
    }
}
