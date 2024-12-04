using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State<TBlackboard> : MonoBehaviour where TBlackboard : EntityBlackboard
{
    protected bool isAnimationFinished;
    protected bool isExitingState;
    protected float startTime;
    protected TBlackboard blackboard;
    protected EntityStateMachine<TBlackboard> stateMachine;

    public void Initialzie(TBlackboard blackboard, EntityStateMachine<TBlackboard> stateMachine)
    {
        this.blackboard = blackboard;
        this.stateMachine = stateMachine;
     
    }
    public virtual void Enter()
    {

        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
    }

    public virtual void Exit()
    {
        isExitingState = true;

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicUpdate()
    {
    }



    public virtual void AnimationTrigger()
    {

    }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;


}
