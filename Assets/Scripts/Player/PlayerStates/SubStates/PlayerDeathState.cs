using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public override void Enter()
    {
        // Chạy animation chết
        blackboard.animator.Play("Death");

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
