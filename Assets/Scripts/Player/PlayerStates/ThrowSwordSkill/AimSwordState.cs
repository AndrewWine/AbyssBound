using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSword : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("AimSword");
        blackboard.skillManager.sword_Skill.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        if(!blackboard.PlayerInputHandler.PressedKeyQ)
        {
        stateMachine.ChangeState(blackboard.throwSword);
        }

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (blackboard.player.transform.position.x > mousePosition.x && blackboard.FacingDirection == 1)
        {
            blackboard.player.Flip();
        }
        else if (blackboard.player.transform.position.x < mousePosition.x && blackboard.FacingDirection == -1)
        {
            blackboard.player.Flip();
        }
        base.LogicUpdate();

    }
}
