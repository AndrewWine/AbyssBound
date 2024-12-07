using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ExitTeleport : EnemyState
{
    public static Action NotifyFindPlace;
    private bool CheclSpellCD;
    private void OnEnable()
    {
        Enemy_DeathBringer.canCastSpell += DoCastSpell;
    }
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
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isAnimationFinished)
        {
            if(CheclSpellCD == true)
                stateMachine.ChangeState(blackboard.startCastSpellState);
            else
                stateMachine.ChangeState(blackboard.enemyDBbattleState);
        }
    }

    public void DoCastSpell(bool check)
    {
        if(check)
        {
            CheclSpellCD = true;
        }

        else { CheclSpellCD = false; }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
