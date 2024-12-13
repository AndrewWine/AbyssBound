using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public Action<int, Transform> playerDeathSFX;
    public override void Enter()
    {
        // Chạy animation chết
        blackboard.animator.Play("Death");
        playerDeathSFX?.Invoke(11, null);
        // Gọi sự kiện chết (nếu có)
        blackboard.player.isDeath?.Invoke();

        // Hiển thị màn hình kết thúc với fadeOut và endText
        GameObject.Find("CanvasUI").GetComponent<UI>().SwithOnEndScreen();
 

        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
}
