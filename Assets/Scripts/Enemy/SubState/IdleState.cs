using UnityEngine;

public class IdleState : EnemyState
{
    public override void Enter()
    {
        base.Enter();
        entity.animator.Play("Idle");
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
            enemystateMachine.ChangeState(entity.battleState);
        }
    }

    public override void PhysicUpdate()
    {
        // Update PlayerCheck based on the raycast result
        hit = Physics2D.Raycast(entity.wallCheck.position, Vector2.right * entity.FacingDirection, entity.enemyData.WallCheckDistance, entity.playerLayer);
        PlayerCheck = hit.collider != null;
    }
}
