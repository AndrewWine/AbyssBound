using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/ Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Moving")]
    public float MovementSpeed = 5;

    public float damage = 10;
    public float magicDamage;

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
