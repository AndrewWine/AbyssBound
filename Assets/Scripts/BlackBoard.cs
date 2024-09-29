using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class BlackBoard : MonoBehaviour
{
    #region Variable
    [Header("Component")]
    public PlayerInputHandler PlayerInputHandler;
    public Animator animator;
    public Rigidbody2D RB;


    [Header("Int Variable")]
    public int FacingDirection;


    [Header("Check Variable")]
    public bool JumpInput;
    public bool DashInput;
    public bool isWall;
    public bool isGrounded;

    [Header("Transform")]
    [SerializeField] public Transform groundCheck;
    [SerializeField] public Transform wallCheck;

    [Header("ColliderSize")]
    public Vector2 originalColliderSize;
    public Vector2 originalColliderOffset;

  


    [Header("States")]
    public PlayerMoveState MoveState;
    public PlayerIdleState idleState;
    public PlayerAirState AirState;
    public PlayerJumpState JumpState;
    public PlayerLandState landState;
    public PlayerWallSlideState WallSlideState;
    public PlayerDashState DashState;
    public PlayerWallJumpState WallJumpState;
    public PlayerSlideState SlideState;
    public PlayerPrimaryAttack primaryAttack;
    public PlayerStateAttack2 primaryAttack2;
    public PlayerStateAttack3 primaryAttack3;
    public RunAttack1 runattack1;
    public RunAttack2 runattack2;
    #endregion

}
