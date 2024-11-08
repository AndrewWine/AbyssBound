using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public override void Enter()
    {
        blackboard.animator.Play("Death");
        blackboard.player.isDeath?.Invoke();

        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
