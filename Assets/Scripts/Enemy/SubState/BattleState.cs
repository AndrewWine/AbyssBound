using Unity.VisualScripting;
using UnityEngine;

public class BattleState : EnemyState
{
    private float previousX; // Lưu tọa độ X của khung hình trước

    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("Walk");
        previousX = blackboard.RB.position.x; // Lưu tọa độ X ban đầu khi vào trạng thái
    }

    public override void LogicUpdate()
    {

        if (!blackboard.playerDetected)
        {
            stateMachine.ChangeState(blackboard.enemyWalkState);
        }
        if (blackboard.isPlayer)
        {
          
                stateMachine.ChangeState(blackboard.enemyAttackState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        // Lấy vị trí hiện tại của enemy
        float currentX = blackboard.RB.position.x;

        // Nếu hướng di chuyển thay đổi, thực hiện lật (flip)
        if ((currentX > previousX && blackboard.FacingDirection == -1) ||
            (currentX < previousX && blackboard.FacingDirection == 1))
        {
            blackboard.enemy.Flip();
        }

        // Lưu lại vị trí X hiện tại để so sánh với khung hình tiếp theo
        previousX = currentX;

        // Di chuyển kẻ địch về phía playerPos
        Vector2 targetPosition = new Vector2(blackboard.targetPlayer.position.x, blackboard.RB.position.y);
        Vector2 newPosition = Vector2.MoveTowards(blackboard.RB.position, targetPosition, blackboard.enemyData.MovementSpeed * 3f * Time.deltaTime);
        blackboard.RB.MovePosition(newPosition);
        Vector2 playerPosition = blackboard.targetPlayer.position;
    
    }

 
}
