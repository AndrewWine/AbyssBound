using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        entity.animator.Play("Walk");
        enemy.SetVelocityY(0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (entity.playerDetected)
        {
            enemystateMachine.ChangeState(entity.battleState);
        }
        if (entity.isWall) enemy.Flip();
        if(!entity.isGrounded) enemy.Flip();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        // Move the enemy in the current facing direction
        enemy.SetVelocityX(enemyData.MovementSpeed * entity.FacingDirection); // Use movement speed property


    }
}
