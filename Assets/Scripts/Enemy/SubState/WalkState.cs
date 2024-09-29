using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        enemy.animator.Play("Walk");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        MoveEnemy();
        if (PlayerCheck)
        {
            enemystateMachine.ChangeState(enemy.enemyChasingState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        // Move the enemy in the current facing direction
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        // Calculate movement vector based on speed and facing direction
        float moveSpeed = enemyData.MovementSpeed; // Get movement speed from EnemyData
        Vector2 movement = new Vector2(enemy.FacingDirection + moveSpeed, 0); // Move only on x-axis

        // Apply movement to the Rigidbody2D
        enemy.RB.velocity = movement;
    }
}
