using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Component")]
    public PlayerBlackBoard blackBoard;
    public PlayerState playerState;
    public PlayerStateMachine stateMachine;
    public PlayerData playerData;
    public UnitHP unitHP;
    public Stats stats;
    public System.Action isDeath;


    private int currentHP;
    private float staminaRegenTimer = 0f; // Timer to track the elapsed time
    private float regenInterval = 1f;   // Interval of 0.5 seconds

    public EntityFX fx { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;

    public void Awake()
    {
        blackBoard = GetComponent<PlayerBlackBoard>();
        fx = GetComponent<EntityFX>();
        StatsInitialization();
        //ebug.Log("Total Damage: " + playerData.DMG.GetValue());
        unitHP.BeingHit += TakeDamage;

    }
    private void OnDisable()
    {
        unitHP.BeingHit -= TakeDamage;

    }

    public void Update()
    {
        CheckOnDrawGizmos();
        CheckIfShouldFlip(blackBoard.PlayerInputHandler.NormInputX);
        RegenStamina();
        Death();
        if (blackBoard.isGrounded)
        {
            // Reset jumps when grounded
            playerData.amountOfJump = 2;
        }
    }

    #region Regen 
    public void RegenStamina()
    {
        if (playerData.CurrentStamina < playerData.MaxStamina)
        {
            staminaRegenTimer += Time.deltaTime;

            if (staminaRegenTimer >= regenInterval)
            {
                playerData.CurrentStamina += 5; // Regenerate 5 stamina points
                if (playerData.CurrentStamina > playerData.MaxStamina)
                {
                    playerData.CurrentStamina = playerData.MaxStamina; // Cap at MaxStamina
                }

                
                staminaRegenTimer = 0f; // Reset the timer after regeneration
            }
        }
    }

    #endregion
    #region stats initialization
    public void StatsInitialization()
    {
        playerData.CurrentMana = playerData.MaxMana;
        playerData.CurrentStamina = playerData.MaxStamina;
        playerData.Damage = 10.0f;
        playerData.evasion = 0.0f;
        playerData.CritChance = 0.0f;
        playerData.CritPower = 1.5f;
        playerData.armor = 0;
        playerData.magicArmor = 0;
        playerData.movementSpeed = 3;
        playerData.MagicDamage = 10;

        blackBoard.FacingDirection = transform.localScale.x > 0 ? 1 : -1;
        unitHP.CurrentHP = unitHP.MaxHP;


    }
    #endregion
    #region Damage

   
    public void TakeDamage()
    {
        //currentHP = Mathf.Clamp(currentHP, 0, playerData.HP.GetValue()); // Đảm bảo không âm và không vượt quá max HP
        fx.StartCoroutine("FlashFX");
    }


    public void AnimationTrigger()
    {
        if (stateMachine != null && stateMachine.CurrentState != null)
        {
            stateMachine.CurrentState.AnimationFinishTrigger();
        }
    }
    #endregion
    #region DeathState
    public void Death()
    {
        if(unitHP.CurrentHP <= 0 )
        {
            stateMachine.ChangeState(blackBoard.playerDeathState);
        }
    }
    #endregion
    #region Flip
    // Improved flip logic
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != blackBoard.FacingDirection)
        {
            Flip();
        }
    }

    public  void Flip()
    {
        // Reverse FacingDirection
        blackBoard.FacingDirection *= -1;

        // Flip the player's local scale to mirror the sprite instead of using rotation
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
    #endregion
    #region Check & DrawGizmos
    public void CheckOnDrawGizmos()
    {
        // Ground check
        blackBoard.isGrounded = Physics2D.Raycast(blackBoard.groundCheck.position, Vector2.down, playerData.groundCheckDistance, playerData.whatIsGround);

        // Wall check
        blackBoard.isWall = Physics2D.Raycast(blackBoard.wallCheck.position, Vector2.right * blackBoard.FacingDirection, playerData.WallCheckDistance, playerData.whatIsGround);

        //Attack check


    }
    public void CheckGizmosCondition()
    {
        // Ground check
        blackBoard.isGrounded = Physics2D.Raycast(blackBoard.groundCheck.position, Vector2.down, playerData.groundCheckDistance, playerData.whatIsGround);
        Debug.Log("Is Grounded: " + blackBoard.isGrounded);

        // Wall check
        blackBoard.isWall = Physics2D.Raycast(blackBoard.wallCheck.position, Vector2.right * blackBoard.FacingDirection, playerData.WallCheckDistance, playerData.whatIsGround);
        Debug.Log("Is Wall: " + blackBoard.isWall + " FacingDirection: " + blackBoard.FacingDirection);
    }
    private void OnDrawGizmos()

    {
        Gizmos.color = Color.yellow;

        // Draw ground check ray
        Vector3 groundCheckPosition = blackBoard.groundCheck.position;
        Gizmos.DrawLine(groundCheckPosition, groundCheckPosition + Vector3.down * playerData.groundCheckDistance);

        // Draw wall check ray
        Vector3 wallCheckPosition = blackBoard.wallCheck.position;
        Gizmos.DrawLine(wallCheckPosition, wallCheckPosition + Vector3.right * blackBoard.FacingDirection * playerData.WallCheckDistance);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(blackBoard.attackCheck.position, playerData.attackCheckRadius);
    }
    #endregion
    #region Setvelocity
    public virtual void SetVelocityZero()
    {
        blackBoard.RB.velocity = Vector2.zero;
        CurrentVelocity = Vector2.zero;
    }

    public virtual void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        blackBoard.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public virtual void SetVelocityY(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        blackBoard.RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion
}
