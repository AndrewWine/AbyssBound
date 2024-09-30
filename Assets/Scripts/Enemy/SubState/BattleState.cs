using Unity.VisualScripting;
using UnityEngine;

public class BattleState : EnemyState
{
    private float previousX; // Lưu tọa độ X của khung hình trước

    public override void Enter()
    {
        base.Enter();
        entity.animator.Play("Walk");
        previousX = entity.RB.position.x; // Lưu tọa độ X ban đầu khi vào trạng thái
    }

    public override void LogicUpdate()
    {

        if (!entity.playerDetected)
        {
            enemystateMachine.ChangeState(entity.enemyWalkState);
        }
        if (entity.isPlayer)
        {
            Debug.Log("Danh dc roi");
                enemystateMachine.ChangeState(entity.enemyAttackState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        // Lấy vị trí hiện tại của enemy
        float currentX = entity.RB.position.x;

        // Nếu hướng di chuyển thay đổi, thực hiện lật (flip)
        if ((currentX > previousX && entity.FacingDirection == -1) ||
            (currentX < previousX && entity.FacingDirection == 1))
        {
            enemy.Flip();
        }

        // Lưu lại vị trí X hiện tại để so sánh với khung hình tiếp theo
        previousX = currentX;

        // Di chuyển kẻ địch về phía player
        Vector2 targetPosition = new Vector2(entity.targetPlayer.position.x, entity.RB.position.y);
        Vector2 newPosition = Vector2.MoveTowards(entity.RB.position, targetPosition, enemyData.MovementSpeed * 3f * Time.deltaTime);
        entity.RB.MovePosition(newPosition);
        Vector2 playerPosition = entity.targetPlayer.position;
    
    }

 
}
