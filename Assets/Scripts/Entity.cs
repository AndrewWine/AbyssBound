using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("States")]
    public IdleState enemyIdleState;
    public AttackState enemyAttackState;
    public HitState enemyHitState;
    public DeathState enemyDeathState;
    public WalkState enemyWalkState;
    public BattleState battleState;

    [Header("Collision infor")]
    public EnemyState enemyState;
    public Animator animator;
    public Rigidbody2D RB;
    public LayerMask playerLayer; // LayerMask for detecting the player
   

    [Header("Check Variables")]
    public bool isWall;
    public bool isGrounded;
    public int FacingDirection;
    public bool playerDetected;
    public bool isPlayer;




    [Header("Transform")]
    [SerializeField] public Transform groundCheck;
    [SerializeField] public Transform wallCheck;
    [SerializeField] public Transform PlayerCheck;
    public Transform attackCheck;
    public Transform targetPlayer;


    [Header("Scriptable Objects")]
    [SerializeField] public EnemyData enemyData;

    

}
