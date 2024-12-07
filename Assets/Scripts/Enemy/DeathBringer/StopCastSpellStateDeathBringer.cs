using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCastSpellStateDeathBringer : EnemyState
{
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("ExitCastSpell");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isAnimationFinished)
        {
            stateMachine.ChangeState(blackboard.enemyDBbattleState);
        }
    }
}
