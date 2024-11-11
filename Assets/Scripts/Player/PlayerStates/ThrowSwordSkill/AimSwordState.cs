using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimSword : PlayerState
{
    public override void Enter()
    {
        base.Enter();
        blackboard.animator.Play("AimSword");
        blackboard.skillManager.sword_Skill.DotsActive(true); // Đảm bảo dots được kích hoạt khi vào trạng thái AimSword
    }

    public override void Exit()
    {
        base.Exit();
        blackboard.skillManager.sword_Skill.DotsActive(false); // Đảm bảo dots được tắt khi thoát khỏi trạng thái AimSword
    }

    public override void LogicUpdate()
    {
        if (!blackboard.PlayerInputHandler.PressedKeyQ)
        {
            stateMachine.ChangeState(blackboard.throwSword); // Chuyển trạng thái khi không còn giữ phím Q
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
