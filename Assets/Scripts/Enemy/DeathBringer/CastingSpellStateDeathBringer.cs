using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingSpellStateDeathBringer : EnemyState
{
    public static System.Action DoSpell;
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("CastingSpell");
        DoSpell?.Invoke();

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
            stateMachine.ChangeState(blackboard.stopCastSpellState);
        }
       
    }
}
