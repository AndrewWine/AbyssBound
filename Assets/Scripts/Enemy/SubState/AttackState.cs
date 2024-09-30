using UnityEngine;
    public class AttackState : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        entity.animator.Play("Attack");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Chỉ thay đổi trạng thái nếu animation đã kết thúc
        if (isAnimationFinished)
        {
            enemystateMachine.ChangeState(entity.battleState);
        }
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        isAnimationFinished = true; // Đánh dấu rằng animation đã kết thúc
        Debug.Log("Animation finished: " + isAnimationFinished);

    }

    public override void Exit()
    {
        base.Exit();
        enemyData.lastTimeAttacked = Time.time;
    }
}
