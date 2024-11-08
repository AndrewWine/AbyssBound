
using UnityEngine;

public class PlayerBlackBoard : EntityBlackboard
{
    #region Variable
    public Player player;
    public SkillsManager skillManager;
    [Header("Collision")]
    public PlayerInputHandler PlayerInputHandler;
    public Animator animator;
    public Rigidbody2D RB;
    public PlayerData playerData;

    [Header("Check Variable of Player")]
    public bool JumpInput;
    public bool DashInput;
    public bool WallDetected;

    [Header("Transform of Player")]
    public Transform attackCheck;

    [Header("KnockBack infor")]
    [SerializeField] protected Vector2 knockbackDirection;
    protected bool isKnocked;

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
    #endregion
}
