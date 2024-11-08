using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : EnemyState
{
    public override void Enter()
    {
        blackboard.animator.Play("Death");
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
