using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/ Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementSpeed = 10f;

    [Header("Time Counter")]
    public float UsageTimer;
    public float PassingTime = 0;

    [Header("Dash State")]
    public float dashCoolDown;
    public float dashSpeed;
    public float dashDuration;

    [Header("AttackState")]
    public int CountAttack = 0;
    public int TimeCondition;
    public int CountClick = 1;

    [Header("SlideState")]
    public float SlideSpeed = 10f;
    public float slideCoolDown;
    public float slideDuration = 0.2f;

    [Header("Jump State")]
    public float jumpVelocity;
    public int amountOfJump;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;

    [Header("Wall slide state")]
    public float wallSlideVelocity = 3;
    public int CurrentFacing;

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Wall Jump State")]
    public float WallJumpForce;


    [Header("Radius controller")]
    public float groundCheckDistance = 0.3f;
    public float WallCheckDistance;
    public float attackCheckRadius;
    public LayerMask whatIsGround;
}
