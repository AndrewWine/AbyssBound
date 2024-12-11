using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WalkState : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("Walk");
        blackboard.enemy.SetVelocityY(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (blackboard.playerDetected)
        {
            stateMachine.ChangeState(blackboard.enemybattleState);
        }
        if (blackboard.isWall) blackboard.enemy.Flip();
        if(!blackboard.isGrounded) blackboard.enemy.Flip();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        // Move the enemy in the current facing direction
        blackboard.enemy.SetVelocityX(blackboard.enemyData.MovementSpeed * blackboard.FacingDirection); // Use movement speed property
    }
}
