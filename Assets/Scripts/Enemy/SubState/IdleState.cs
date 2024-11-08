using UnityEngine;

public class IdleState : EnemyState
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
            stateMachine.ChangeState(blackboard.enemybattleState);
        }
    }

    public override void PhysicUpdate()
    {
 
    }
}
