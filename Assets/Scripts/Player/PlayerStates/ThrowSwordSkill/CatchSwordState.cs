using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchSwordState : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("CatchSword");
        Debug.Log("CatchSword animation started.");
    }

    public override void AnimationFinishTrigger()
    {
        isAnimationFinished = true;
        Debug.Log("CatchSword animation finished.");
        base.AnimationFinishTrigger();
    }

    public override void LogicUpdate()
    {
        if (isAnimationFinished)
        {
            Debug.Log("Transitioning to Idle State.");
            stateMachine.ChangeState(blackboard.idleState);
        }
        base.LogicUpdate();
    }

}
