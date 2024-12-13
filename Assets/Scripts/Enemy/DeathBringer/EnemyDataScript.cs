using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataScript : MonoBehaviour
{
    [Header("Stats")]
    public float MovementSpeed = 4.5f;
    public float MaxHP;
    public float damage;
    public float magicDamage;
    public float Armor = 1;
    public float MagicArmor;
    public float Level;

    [Header("Soul drop")]
    public float AbyssEssenceDropAmount;
    [Header("Check Variables")]
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    [Header("Attack Infor")]
    public float attackCooldown;
    public float lastTimeAttacked;

    [Header("Radius")]
    public float attackCheckRadius;
    public float groundCheckDistance = 0.3f;
    public float WallCheckDistance;
    public float PlayerCheckDistance;
}
