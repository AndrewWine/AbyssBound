using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateDeathBringer : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("Idle");
    }

    public override void Exit()
    {
        base.Exit();
        // Removed the PlayerCheck here
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // Check for player detection in LogicUpdate
        if (blackboard.playerDetected)
        {
            stateMachine.ChangeState(blackboard.enemyDBbattleState);
        }
    }

    public override void PhysicUpdate()
    {

    }
}
