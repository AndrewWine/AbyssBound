using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("Stun");
        blackboard.enemy.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);
        startTime = blackboard.stunDuration;
        blackboard.RB.velocity= new Vector2(-blackboard.FacingDirection * blackboard.stunDirection.x, blackboard.stunDirection.y);
    }


    public override void LogicUpdate()
    {
        startTime -= Time.deltaTime;
        base.LogicUpdate();
        if(startTime < 0)
        {
            stateMachine.ChangeState(blackboard.enemyIdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        blackboard.enemy.fx.Invoke("CancelRedBlink",0);    
    }

}
