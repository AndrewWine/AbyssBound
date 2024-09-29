using UnityEngine;

public class IdleState : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        enemy.animator.Play("Idle");
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
        if (PlayerCheck)
        {
            enemystateMachine.ChangeState(enemy.enemyChasingState);
        }
    }

    public override void PhysicUpdate()
    {
        // Update PlayerCheck based on the raycast result
        hit = Physics2D.Raycast(enemy.wallCheck.position, Vector2.right * enemy.FacingDirection, enemy.enemyData.WallCheckDistance, enemy.playerLayer);
        PlayerCheck = hit.collider != null;
    }
}
