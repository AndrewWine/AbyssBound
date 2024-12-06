using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastingSpellStateDeathBringer : EnemyState
{
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

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!isAnimationFinished)
        {

        }    
    }
}
