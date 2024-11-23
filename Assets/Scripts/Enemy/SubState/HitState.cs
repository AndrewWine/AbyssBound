using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : EnemyState
{
    public override void Enter()
    {
        blackboard.animator.Play("Hit");
        base.Enter();
    }

    public override void LogicUpdate()
    {
        stateMachine.ChangeState(blackboard.enemyWalkState);
        base.LogicUpdate();
    }
}
