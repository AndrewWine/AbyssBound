using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathState : EnemyState
{
    public static Action ClearHPBar;
    public static Action<int, Transform> deathSFX;

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        // Chạy animation hoặc logic chết
        blackboard.animator.Play("Death");
        ClearHPBar?.Invoke();
        deathSFX?.Invoke(24, null);
        // Khóa trạng thái
        stateMachine.LockState();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        // Logic của trạng thái chết (nếu cần)
    }
}
