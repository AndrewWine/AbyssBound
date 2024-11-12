using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/ Base Data")]
public class PlayerData : ScriptableObject
{
   
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

    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = 0.2f;

    [Header("Radius controller")]
    public float groundCheckDistance;
    public float WallCheckDistance;
    public float attackCheckRadius;
    public LayerMask whatIsGround;

    [Header("Player Stats")]
    public float MaxMana = 20;
    public float CurrentMana;
    public float MaxStamina = 30;
    public float CurrentStamina;

    [Header("Major Stats")]
    public float strength; //1 point increase damage by 1 and crit.power by 1%
    public float agility;  // 1 point increase evasion by 1%, move speed 1% and crit.chance by 1%
    public float intelligence;// 1 point increase magic damage by 1, magic resistance by 3 and MaxMana by 2
    public float vitallity;// 1 point increase health by 5 point and increase stamina by 3 point

    [Header("Offensive Staff")]
    public float Damage;
    public float MagicDamage;
    public float CritChance;
    public float CritPower;     //default value = 150%


    [Header("Defensive stats")]    
    public float armor;
    public float magicArmor;
    public float evasion;

    [Header("Other stats")]
    public float movementSpeed;

    [Header("Buff")]
    public float staminaRegenRate;
    public float hpRegenRate;
    public float manaRegenRate;

    [Header("Effect")]
    public float lifesteal;
    public float canIgnite;
    public float canFreaze;
    public float canShock;
    public bool Ignite; //cause injury per second (the more attack the more time injury per second)
    public bool Freaze; // have a chance to freaze target ( freaze = stun + effect)
    public bool Shock; // Cause unable to move increase crit chance (target still able to attack)
    public bool Chill; //slowdown movementspeed have ability to become Freaze, if target is Chill and player attack with ability freaze then freaze target
}
