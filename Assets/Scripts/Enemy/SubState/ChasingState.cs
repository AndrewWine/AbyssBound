using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        enemy.animator.Play("Walk");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
