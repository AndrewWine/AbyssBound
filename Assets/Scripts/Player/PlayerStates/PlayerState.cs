using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
 
    protected PlayerBlackBoard blackBoard;
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;
    protected SkillsManager skillsManager;
    protected bool isAnimationFinished;
    protected bool isExitingState;
    protected float startTime;
    


    public void Initialzie(PlayerBlackBoard blackBoard, Player player, PlayerStateMachine stateMachine, PlayerData playerData, SkillsManager skillsManager )
    {
       this.blackBoard = blackBoard;
       this.player = player;   
       this.stateMachine = stateMachine;
       this.playerData = playerData;
       this.skillsManager = skillsManager;
    }




    public virtual void Enter()
    {
        skillsManager = GameObject.FindObjectOfType<SkillsManager>(); 
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

    public virtual void AnimationFinishTrigger()  => isAnimationFinished = true;




}
