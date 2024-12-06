using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class EntityBlackboard : MonoBehaviour
{
    public GameObject statesParent;
    public Enemy enemy;
    [Header("Scriptable Objects")]
    [SerializeField] public EnemyData enemyData;
    [Header("Check variable")]
    public bool isWall;
    public bool isGrounded;
    public int FacingDirection;

    [Header("Transform")]
    [SerializeField] public Transform groundCheck;
    [SerializeField] public Transform wallCheck;
    public Transform PlayerCheck;

    [Header("Transform of Enemy")]
    public Transform attackCheck;
    public Transform targetPlayer;

    [Header("Collision infor")]
    public Animator animator;
    public Rigidbody2D RB;
    public LayerMask playerLayer; // LayerMask for detecting the player

    [Header("Stun infor")]
    public float stunDuration;
    public Vector2 stunDirection;
    public bool canBeStunned;
    [SerializeField] public GameObject counterImage;

    [Header("Check Variables of Enemy")]
    public bool isDeathBringer;
    public bool playerDetected;
    public bool isPlayer;

    [Header("Other Variable")]
    public int countAtk;

    [Header("KnockBack infor")]
    [SerializeField] public Vector2 knockbackDirection;
    [SerializeField] public float knockbackDuration;

    public bool isKnocked;
    [Header("Default State")]
    public IdleState enemyIdleState;
    public AttackState enemyAttackState;
    public HitState enemyHitState;
    public DeathState enemyDeathState;
    public WalkState enemyWalkState;
    public BattleState enemybattleState;
    public StunState enemystunState;
    [Header("DeathBringer state")]
    public IdleStateDeathBringer enemyDBIdleState;
    public AttackStateDeathBringer enemyDBAttackState;
    public HitStateDeathBringer enemyDBHitState;
    public DeathStateDeathBringer enemyDBDeathState;
    public WalkStateDeathBringer enemyDBWalkState;
    public BattleStateDeathBringer enemyDBbattleState;
    //DeathBringer State
    public CastingSpellStateDeathBringer castingSpellState;
    public CastSpellStateDeathBringer startCastSpellState;
    public StopCastSpellStateDeathBringer stopCastSpellState;
    public EnterTeleport enterTeleportDeathBringer;
    public ExitTeleport exitTeleportDeathBringer;
}


