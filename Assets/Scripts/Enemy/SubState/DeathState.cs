using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathState : EnemyState
{

    public override void Enter()
    {
        base.Enter();

        // Chạy animation hoặc logic chết
        blackboard.animator.Play("Death");

        // Khóa trạng thái
        stateMachine.LockState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Logic của trạng thái chết (nếu cần)
    }
}
