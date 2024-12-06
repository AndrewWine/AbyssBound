
using UnityEngine;

public class PlayerBlackBoard : EntityBlackboard
{
    #region Variable
    public Player player;
    public SkillsManager skillManager;
    [Header("Collision")]
    public PlayerInputHandler PlayerInputHandler;
    public PlayerData playerData;
    

    [Header("Check Variable of Player")]
    public bool JumpInput;
    public bool DashInput;
    public bool WallDetected;


  

    [Header("ColliderSize")]
    public Vector2 originalColliderSize;
    public Vector2 originalColliderOffset;

  


    [Header("States")]
    public PlayerMoveState MoveState;
    public PlayerIdleState idleState;
    public PlayerAirState AirState;
    public PlayerJumpState JumpState;
    public PlayerWallSlideState WallSlideState;
    public PlayerDashState DashState;
    public PlayerWallJumpState WallJumpState;
    public PlayerSlideState SlideState;
    public PlayerPrimaryAttack primaryAttack;
    public PlayerStateAttack2 primaryAttack2;
    public PlayerStateAttack3 primaryAttack3;
    public RunAttack1 runattack1;
    public RunAttack2 runattack2;
    public PlayerCounterAttack counterattack;
    public AimSword aimSword;
    public ThrowSwordState throwSword;
    public CatchSwordState catchSword;
    public PlayerDeathState playerDeathState;
    public AirAttack playerairAttack;
    public FallBack fallBack;
    #endregion
}
