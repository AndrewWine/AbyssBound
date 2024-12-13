
using UnityEngine;
using System;
public class Player : MonoBehaviour
{
    public Action<bool> DropCurrency;
    public Action isDeath;
    [Header("Component")]
    public PlayerBlackBoard blackBoard;
    public PlayerState playerState;
    public PlayerStateMachine stateMachine;
    public PlayerData playerData;
    public Stats stats;



    private int currentHP;
    private float RegenTimer = 0f; // Timer to track the elapsed time
    private float regenInterval = 0.1f;   // Interval of 0.5 seconds

    public EntityFX fx { get; private set; }

    public Vector2 CurrentVelocity { get; private set; }
    private Vector2 workspace;

    public void Awake()
    {
        blackBoard = GetComponent<PlayerBlackBoard>();
        fx = GetComponent<EntityFX>();
        PlayerStatsInfor();
        //ebug.Log("Total Damage: " + playerData.DMG.GetValue());

    }

    public void Update()
    {
        CheckOnDrawGizmos();
        CheckIfShouldFlip(blackBoard.PlayerInputHandler.NormInputX);
        RegenStamina();
        RegenHP();
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
            RegenTimer += Time.deltaTime;

            if (RegenTimer >= regenInterval)
            {
                playerData.CurrentStamina += playerData.staminaRegenRate; // Regenerate 5 stamina points
                if (playerData.CurrentStamina > playerData.MaxStamina)
                {
                    playerData.CurrentStamina = playerData.MaxStamina; // Cap at MaxStamina
                }

                
                RegenTimer = 0f; // Reset the timer after regeneration
            }
        }
    }

    public void RegenHP()
    {
        if (playerData.CurrentHP < playerData.MaxHP)
        {
            RegenTimer += Time.deltaTime;

            if (RegenTimer >= regenInterval)
            {
                playerData.CurrentHP += playerData.lifesteal * playerData.Damage; // Regenerate 5 stamina points
                if (playerData.CurrentStamina > playerData.MaxStamina)
                {
                    playerData.CurrentStamina = playerData.MaxStamina; // Cap at MaxStamina
                }


                RegenTimer = 0f; // Reset the timer after regeneration
            }
        }
    }

    #endregion
    #region stats initialization
    public void PlayerStatsInfor()
    {
        playerData.CurrentMana = playerData.MaxMana ;
        playerData.CurrentStamina = playerData.MaxStamina ;
        playerData.MaxMana = playerData.intelligence * 2 +30;
        playerData.MaxStamina = playerData.vitallity * 3 + 50;
        playerData.MaxHP = playerData.vitallity * 5 + 100;
        playerData.Damage = playerData.strength + 10;
        playerData.evasion = playerData.agility * 0.1f;
        playerData.CritChance = playerData.agility * 0.1f ;
        playerData.CritPower = playerData.strength*0.1f + 1.5f;
        playerData.movementSpeed = 3 + playerData.agility * 0.1f;
        playerData.MagicDamage = 10;

        //stats just can change by item
        playerData.armor = 0;
        playerData.magicArmor = 0;
        playerData.staminaRegenRate = 1;

        blackBoard.FacingDirection = transform.localScale.x > 0 ? 1 : -1;
        playerData.CurrentHP = playerData.MaxHP;


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
        if(playerData.CurrentHP <= 0 )
        {
            stateMachine.ChangeState(blackBoard.playerDeathState);
            DropCurrency?.Invoke(true);
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
