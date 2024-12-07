using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateDeathBringer : EnemyState
{
    private float previousX;
    private Enemy_DeathBringer enemy;
    private int amountOfSpells;
    private float spellTimer;
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy_DeathBringer>();
    }
    public override void Enter()
    {
        base.Enter();
        previousX = blackboard.RB.position.x;
        blackboard.animator.Play("Walk");
        amountOfSpells = enemy.amountOfspells;
        spellTimer =  0.5f;
    }

    public override void LogicUpdate()
    {
        spellTimer -= Time.deltaTime;
        if (!blackboard.playerDetected && !CanCast())
        {
            stateMachine.ChangeState(blackboard.enterTeleportDeathBringer);
        }

        else if (blackboard.isPlayer && blackboard.enemy.countAttack < 5 && blackboard.enemy.CanAttack() && !CanCast())
        {
            stateMachine.ChangeState(blackboard.enemyDBAttackState);
        }
        else if (CanCast())
        {
            stateMachine.ChangeState(blackboard.startCastSpellState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();

        float currentX = blackboard.RB.position.x;

        // Kiểm tra nếu hướng di chuyển thay đổi, thực hiện lật hướng
        if ((currentX > previousX && blackboard.FacingDirection == -1) ||
            (currentX < previousX && blackboard.FacingDirection == 1))
        {
            blackboard.enemy.Flip();
        }

        previousX = currentX;

        // Lấy vị trí của player và tính khoảng cách
        Vector2 playerPosition = new Vector2(blackboard.targetPlayer.position.x, blackboard.RB.position.y);
        float distanceToPlayer = Vector2.Distance(blackboard.RB.position, playerPosition);

        // Nếu còn cách player lớn hơn 3f, tiếp tục di chuyển
        if (distanceToPlayer > 3f)
        {
            Vector2 newPosition = Vector2.MoveTowards(blackboard.RB.position, playerPosition, blackboard.enemyData.MovementSpeed * Time.deltaTime);
            blackboard.RB.MovePosition(newPosition);
        }
        else
        {
            // Dừng di chuyển nếu đã đến gần player trong phạm vi 3f
            blackboard.enemy.SetVelocityZero();
        }
    }

    private bool CanAttack()
    {
        if(Time.time >= blackboard.enemy.lastTimeAttacked + blackboard.enemy.attackCooldown)
        {
            blackboard.enemy.attackCooldown = Random.Range(1, 4);
            blackboard.enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }

    private bool CanCast()
    {
        if (amountOfSpells > 0 && spellTimer < 0)
        {
            spellTimer = enemy.spellCoolDown;
            return true;
        }
        return false;
    }

}
