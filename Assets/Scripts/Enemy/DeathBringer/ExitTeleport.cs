using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ExitTeleport : EnemyState
{
    public static Action NotifyFindPlace;
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("ExitTeleport");
        NotifyFindPlace?.Invoke();//Dk trong deathbringer script

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
            stateMachine.ChangeState(blackboard.startCastSpellState);
        }
    }
}
