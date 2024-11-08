using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class 
 EnemyBlackBoard : EntityBlackboard
{
    public Enemy enemy;

    [Header("States")]
    public IdleState enemyIdleState;
    public AttackState enemyAttackState;
    public HitState enemyHitState;
    public DeathState enemyDeathState;
    public WalkState enemyWalkState;
    public BattleState enemybattleState;
    public StunState enemystunState;

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
    
    public bool playerDetected;
    public bool isPlayer;

    [Header("KnockBack infor")]
    [SerializeField] public Vector2 knockbackDirection;
    [SerializeField] public float knockbackDuration;

    public bool isKnocked;

    [Header("Transform of Enemy")]
    public Transform attackCheck;
    public Transform targetPlayer;


    [Header("Scriptable Objects")]
    [SerializeField] public EnemyData enemyData;

}
