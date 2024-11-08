using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSwordState : PlayerState
{
  

    public override void Enter()
    {

        blackboard.animator.Play("ThrowSword");
        blackboard.skillManager.ActivateThrowSword();
        base.Enter();
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
            Debug.Log("Throwing");
            stateMachine.ChangeState(blackboard.idleState);

        }
    }
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();  
        isAnimationFinished = true;
    }
}
