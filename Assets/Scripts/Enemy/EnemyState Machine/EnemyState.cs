using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    protected Entity entity;
    protected Enemy enemy;
    protected EnemyStateMachine enemystateMachine;
    protected EnemyData enemyData;
    protected bool isAnimationFinished;
    protected bool isExitingState;
    protected float startTime;


    public bool PlayerCheck;
    public RaycastHit2D hit;



    public void Initialzie(Entity entity, Enemy enemy, EnemyStateMachine stateMachine, EnemyData enemyData)
    {
        this.entity = entity;
        this.enemy = enemy;
        this.enemystateMachine = stateMachine;
        this.enemyData = enemyData;
    }




    public virtual void Enter()
    {

        startTime = Time.time;
        Debug.Log(enemystateMachine.CurrentState);
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
