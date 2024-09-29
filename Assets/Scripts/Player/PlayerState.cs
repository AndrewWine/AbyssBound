using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
 
    protected BlackBoard blackBoard;
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected bool isAnimationFinished;
    protected bool isExitingState;
    protected float startTime;
    


    public void Initialzie(BlackBoard blackBoard, Player player, PlayerStateMachine stateMachine, PlayerData playerData )
    {
       this.blackBoard = blackBoard;
       this.player = player;   
       this.stateMachine = stateMachine;
       this.playerData = playerData;
    }




    public virtual void Enter()
    {
       
        startTime = Time.time;
        Debug.Log(stateMachine.CurrentState);
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

    public virtual void AnimationFinishTrigger()  => isAnimationFinished = true;




}
