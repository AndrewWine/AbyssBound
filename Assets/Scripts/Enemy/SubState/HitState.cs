using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : EnemyState
{
    public static System.Action<int, Transform> BeingHit;
    public override void Enter()
    {
        BeingHit?.Invoke(24,null);
        blackboard.animator.Play("Hit");
        base.Enter();
    }

    public override void LogicUpdate()
    {
        stateMachine.ChangeState(blackboard.enemyWalkState);
        base.LogicUpdate();
    }
}
