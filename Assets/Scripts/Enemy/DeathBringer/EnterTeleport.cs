
using System;
public class EnterTeleport : EnemyState
{


    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        
        base.Enter();
        blackboard.enemy.countAttack = 0;
        blackboard.animator.Play("EnterTeleport");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isAnimationFinished)
            stateMachine.ChangeState(blackboard.exitTeleportDeathBringer);
    }
}
